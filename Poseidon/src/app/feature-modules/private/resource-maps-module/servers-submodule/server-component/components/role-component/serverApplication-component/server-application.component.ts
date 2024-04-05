import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { HandlerResponse } from 'src/app/core/models/handlerResponse';
import { ServerApplication } from '../../../models/serverApplication';
import { RoleService } from '../../../services/role.service';
import { ServerApplicationService } from '../../../services/server-application.service';
import { Role } from '../../../models/role';
import { PopupConfirmationComponent } from 'src/app/shared/components/popups/popup-confirmation/popup-confirmation.component';
import { ServerService } from '../../../services/server.service';
import { ApplicationService } from 'src/app/feature-modules/private/resource-maps-module/applications-submodule/application-component/services/application.service';
import { Application } from 'src/app/feature-modules/private/resource-maps-module/applications-submodule/application-component/models/application';
import { SubapplicationService } from 'src/app/feature-modules/private/resource-maps-module/applications-submodule/application-component/services/subapplication.service';
import { SubApplication } from 'src/app/feature-modules/private/resource-maps-module/applications-submodule/application-component/models/subapplications';
import { ComponentModel } from 'src/app/feature-modules/private/resource-maps-module/applications-submodule/application-component/models/component';
import { ComponentService } from 'src/app/feature-modules/private/resource-maps-module/applications-submodule/application-component/services/component.service';
import { Ng7BootstrapBreadcrumbService } from 'ng7-bootstrap-breadcrumb';
import { tap } from 'rxjs';

@Component({
  selector: 'app-server-application',
  templateUrl: './server-application.component.html',
  styleUrls: ['./server-application.component.css']
})
export class ServerApplicationComponent implements OnInit {

  serverApplicationForm: FormGroup;
  serverApplications: ServerApplication[];
  serverApplicationToAdd: ServerApplication;
  idServer: string;
  idRole: string;
  nameRole: string;
  roles: Role[];
  applications: Application[];
  subApplications: SubApplication[];
  components: ComponentModel[];
  result: HandlerResponse;
  page: number;
  pageSize: number;
  loaded: boolean;
  searchText!: string;

  constructor(private formBuilder: FormBuilder, private serverApplicationService: ServerApplicationService, private roleService: RoleService,
    private aRoute: ActivatedRoute, private serverService: ServerService, private applicationService: ApplicationService,
    private subApplicationService: SubapplicationService, private componentService: ComponentService, public dialog: MatDialog,
    private ng7BootstrapBreadcrumbService: Ng7BootstrapBreadcrumbService) {
    this.serverApplicationForm = this.formBuilder.group({
      id: [''],
      roleId: [''],
      componentId: [''],
      hasComponent: ['', Validators.required],
      applicationId: [''],
      subapplicationId: [''],
      name: ['', Validators.required],
      active: [true, Validators.required]
    });
    this.aRoute.snapshot.paramMap.get('idServer');
    this.idServer = this.aRoute.snapshot.paramMap.get('idServer')!;
    this.aRoute.snapshot.paramMap.get('idRole');
    this.idRole = this.aRoute.snapshot.paramMap.get('idRole')!;
    this.nameRole = '';
    this.roles = [];
    this.serverApplications = [];
    this.applications = [];
    this.subApplications = [];
    this.components = [];
    this.serverApplicationToAdd = new ServerApplication();
    this.result = new HandlerResponse();
    this.loaded = false;
    this.page = 1;
    this.pageSize = 25;
  }

  ngOnInit(): void {
    this.getServerById();
    this.getRoleById();
    this.getAllApplications();
    this.getAllServerApplications();
  }

  getServerById() {
    this.serverService.getServerById(this.idServer)
      .subscribe(response => {
        const breadcrumb = { serverName: this.result.getData(response).name };
        this.ng7BootstrapBreadcrumbService.updateBreadcrumbLabels(breadcrumb);
      });
  }

  getRoleById() {
    this.roleService.getRoleById(this.idServer, this.idRole)
      .subscribe(response => {
        this.nameRole = this.result.getData(response).name;
        const breadcrumb = { roleName: this.result.getData(response).name };
        this.ng7BootstrapBreadcrumbService.updateBreadcrumbLabels(breadcrumb);
      });
  }

  getAllApplications() {
    this.applicationService.getAllApplications()
      .subscribe(response => {
        console.log(response);
        this.applications = this.result.getData(response);
      });
  }

  getAllSubApplications() {
    this.subApplicationService.getAllSubApplications(this.serverApplicationForm.value.applicationId)
      .subscribe(response => {
        console.log(response);
        this.subApplications = this.result.getData(response);
      });
  }

  getAllComponents() {
    this.componentService.getAllComponents(this.serverApplicationForm.value.applicationId, this.serverApplicationForm.value.subapplicationId)
      .subscribe(response => {
        console.log(response);
        this.components = this.result.getData(response);
      });
  }

  changeHasComponent(event: Event) {
    if (this.serverApplicationForm.value.hasComponent) {
      this.serverApplicationForm.get('applicationId')?.setValidators(Validators.required);
      this.serverApplicationForm.get('subapplicationId')?.setValidators(Validators.required);
      this.serverApplicationForm.get('componentId')?.setValidators(Validators.required);
    }
    else {
      this.serverApplicationForm.get('applicationId')?.clearValidators();
      this.serverApplicationForm.get('subapplicationId')?.clearValidators();
      this.serverApplicationForm.get('componentId')?.clearValidators();
    }
    this.serverApplicationForm.get('applicationId')?.updateValueAndValidity();
    this.serverApplicationForm.get('subapplicationId')?.updateValueAndValidity();
    this.serverApplicationForm.get('componentId')?.updateValueAndValidity();
  }

  changeApplicationSelected(event: Event) {
    this.getAllSubApplications();
  }

  changeSubApplicationSelected(event: Event) {
    this.getAllComponents();
  }

  getAllServerApplications() {
    this.serverApplicationService.getAllServerApplications(this.idServer, this.idRole)
      .pipe(
        tap(() => (this.loaded = false)),
      )
      .subscribe(response => {
        console.log(response);
        this.serverApplications = this.result.getData(response);
        this.loaded = true;
      });
  }

  addServerApplication() {
    this.serverApplicationToAdd.name = this.serverApplicationForm.value.name;
    this.serverApplicationToAdd.roleId = this.idRole;
    this.serverApplicationToAdd.componentId = this.serverApplicationForm.value.hasComponent ? this.serverApplicationForm.value.componentId : null;
    this.serverApplicationToAdd.active = this.serverApplicationForm.value.active;
    this.serverApplicationService.addServerApplication(this.idServer, this.idRole, this.serverApplicationToAdd)
      .subscribe(response => {
        console.log(this.result.getData(response));
        this.getAllServerApplications();
      });
  }

  updateServerApplication() {

  }

  modifyServerApplication(serverApplication: ServerApplication) {

  }

  deleteServerApplication(serverApplication: ServerApplication) {
    this.serverApplicationService.deleteServerApplication(this.idServer, this.idRole, serverApplication.id)
      .subscribe(response => {
        console.log(response);
        this.getAllServerApplications();
      });
  }

  openDialog(serverApplication: ServerApplication): void {
    const dialogRef = this.dialog.open(PopupConfirmationComponent, {
      width: '350px',
      data: "Are you sure that you want to delete this server application?"
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log("yes");
        this.deleteServerApplication(serverApplication);
      }
    })
  }

  resetFormValues(): void {
    this.serverApplicationForm.reset();
    this.serverApplicationForm.controls['active'].setValue(true);
  }

}
