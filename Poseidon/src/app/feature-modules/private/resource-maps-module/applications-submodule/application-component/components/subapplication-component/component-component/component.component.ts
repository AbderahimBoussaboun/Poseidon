import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { Ng7BootstrapBreadcrumbService } from 'ng7-bootstrap-breadcrumb';
import { tap } from 'rxjs';
import { HandlerResponse } from 'src/app/core/models/handlerResponse';
import { Balancer } from 'src/app/feature-modules/private/resource-maps-module/balancers-submodule/balancer-component/models/balancer';
import { BalancerService } from 'src/app/feature-modules/private/resource-maps-module/balancers-submodule/balancer-component/services/balancer.service';
import { PopupConfirmationComponent } from 'src/app/shared/components/popups/popup-confirmation/popup-confirmation.component';
import { ComponentType } from '../../../../componentType-component/models/componentType';
import { ComponentTypeService } from '../../../../componentType-component/services/component-type.service';
import { ComponentModel } from '../../../models/component';
import { ApplicationService } from '../../../services/application.service';
import { ComponentService } from '../../../services/component.service';
import { SubapplicationService } from '../../../services/subapplication.service';

@Component({
  selector: 'app-component',
  templateUrl: './component.component.html',
  styleUrls: ['./component.component.css']
})
export class ComponentComponent implements OnInit {

  componentForm: FormGroup;
  components: ComponentModel[];
  componentToAdd: ComponentModel;
  idApplication: string;
  idSubApplication: string;
  componentsType: ComponentType[];
  balancers: Balancer[];
  nameSubApplication: string;
  result: HandlerResponse;
  searchText!: string;
  page: number;
  pageSize: number;
  loaded: boolean;

  constructor(private formBuilder: FormBuilder, private componentService: ComponentService, private componentTypeService: ComponentTypeService,
    private balancerService: BalancerService, private subapplicationService: SubapplicationService, private aRoute: ActivatedRoute, 
    public dialog: MatDialog, private applicationService: ApplicationService, private ng7BootstrapBreadcrumbService: Ng7BootstrapBreadcrumbService) {
    this.componentForm = this.formBuilder.group({
      id: [''],
      name: ['', Validators.required],
      ip: ['', Validators.required],
      ports: ['', Validators.required],
      queryString: ['', Validators.required],
      balancerId: [''],
      hasBalancer: ['', Validators.required],
      componentTypeId: ['', Validators.required],
      subApplicationId: [''],
      active: [true, Validators.required]
    });
    this.aRoute.snapshot.paramMap.get('idApplication');
    this.idApplication = this.aRoute.snapshot.paramMap.get('idApplication')!;
    this.aRoute.snapshot.paramMap.get('idSubApplication');
    this.idSubApplication = this.aRoute.snapshot.paramMap.get('idSubApplication')!;
    this.nameSubApplication = '';
    this.componentsType = [];
    this.balancers = [];
    this.components = [];
    this.loaded = false;
    this.componentToAdd = new ComponentModel();
    this.result = new HandlerResponse();
    this.page = 1;
    this.pageSize = 25;
  }

  ngOnInit(): void {
    this.getApplicationById();
    this.getAllComponentsType();
    this.getComponentById();
    this.getAllComponents();
    this.getAllBalancers();
  }

  getAllComponents() {
    this.componentService.getAllComponents(this.idApplication, this.idSubApplication)
      .pipe(
        tap(() => (this.loaded = false)),
      )
      .subscribe(response => {
        console.log(response);
        this.components = this.result.getData(response);
        this.loaded = true;
      });
  }

  getApplicationById() {
    this.applicationService.getApplicationById(this.idApplication)
      .subscribe(response => {
        const breadcrumb =  {applicationName: this.result.getData(response).name};
        this.ng7BootstrapBreadcrumbService.updateBreadcrumbLabels(breadcrumb);
      });
  }

  getComponentById() {
    this.subapplicationService.getSubApplicationById(this.idApplication, this.idSubApplication)
      .subscribe(response => {
        this.nameSubApplication = this.result.getData(response).name;
        const breadcrumb =  {subapplicationName: this.nameSubApplication};
        this.ng7BootstrapBreadcrumbService.updateBreadcrumbLabels(breadcrumb);
      });
  }

  getAllComponentsType() {
    this.componentTypeService.getAllComponentTypes()
      .subscribe(response => {
        this.componentsType = this.result.getData(response);
      });
  }

  getAllBalancers() {
    this.balancerService.getAllBalancers()
      .subscribe(response => {
        this.balancers = this.result.getData(response);
      });
  }

  changeHasBalancer(event: Event){
    if(this.componentForm.value.hasBalancer){
      this.componentForm.get('balancerId')?.setValidators(Validators.required);
    }
    else {
      this.componentForm.get('balancerId')?.clearValidators();
    }
    this.componentForm.get('balancerId')?.updateValueAndValidity();
  }

  addComponent() {
    console.log(this.componentForm);
    this.componentToAdd.name = this.componentForm.value.name;
    this.componentToAdd.ip = this.componentForm.value.ip;
    this.componentToAdd.queryString = this.componentForm.value.queryString;
    var portsSplitted = this.componentForm.value.ports.split(",");
    this.componentToAdd.ports = portsSplitted;
    this.componentToAdd.componentTypeId = this.componentForm.value.componentTypeId;
    this.componentToAdd.balancerId = this.componentForm.value.hasBalancer ? this.componentForm.value.balancerId : null;
    this.componentToAdd.subapplicationId = this.idSubApplication;
    this.componentToAdd.active = this.componentForm.value.active;
    this.componentService.addComponent(this.idApplication, this.idSubApplication, this.componentToAdd)
      .subscribe(response => {
        console.log(response);
        this.getAllComponents();
      });
  }

  modifyComponent(component: ComponentModel) {
    this.componentForm.controls['id'].setValue(component.id);
    this.componentForm.controls['name'].setValue(component.name);
    this.componentForm.controls['ip'].setValue(component.ip);
    this.componentForm.controls['subApplicationId'].setValue(this.idSubApplication);
    this.componentForm.controls['componentTypeId'].setValue(component.componentTypeId);
    this.componentForm.controls['hasBalancer'].setValue(component.balancerId !== null ? true : false);
    this.componentForm.controls['balancerId'].setValue(component.balancerId !== null ? component.balancerId : null);
    this.componentForm.controls['ports'].setValue(component.ports);
    this.componentForm.controls['queryString'].setValue(component.queryString);
    this.componentForm.controls['active'].setValue(component.active);
  }

  updateComponent() {
    this.componentToAdd.id = this.componentForm.value.id;
    this.componentToAdd.name = this.componentForm.value.name;
    this.componentToAdd.ip = this.componentForm.value.ip;
    this.componentToAdd.subapplicationId = this.componentForm.value.subApplicationId;
    this.componentToAdd.componentTypeId = this.componentForm.value.componentTypeId;
    var portsFormValue = this.componentForm.value.ports;
    var portsSplitted = typeof portsFormValue === 'string' ? portsFormValue.split(",") : portsFormValue;
    this.componentToAdd.ports = portsSplitted;
    this.componentToAdd.queryString = this.componentForm.value.queryString;
    this.componentToAdd.active = this.componentForm.value.active;
    this.componentToAdd.balancerId = this.componentForm.value.hasBalancer ? this.componentForm.value.balancerId : null;
    console.log(this.componentToAdd);
    this.componentService.updateComponent(this.idApplication, this.idSubApplication, this.componentToAdd.id, this.componentToAdd)
      .subscribe(response => {
        console.log(response);
        this.getAllComponents();
      });
  }

  deleteComponent(component: ComponentModel) {
    this.componentService.deleteComponent(this.idApplication, this.idSubApplication, component.id)
      .subscribe(response => {
        console.log(response);
        this.getAllComponents();
      });
  }

  openDialog(component: ComponentModel) {
    const dialogRef = this.dialog.open(PopupConfirmationComponent, {
      width: '350px',
      data: "Are you sure that you want to delete this Component?"
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log("yes");
        this.deleteComponent(component);
      }
    })
  }

  resetFormValues(): void {
    this.componentForm.reset();
    this.componentForm.controls['active'].setValue(true);
  }

}
