import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { Ng7BootstrapBreadcrumbService } from 'ng7-bootstrap-breadcrumb';
import { tap } from 'rxjs';
import { HandlerResponse } from 'src/app/core/models/handlerResponse';
import { PopupConfirmationComponent } from 'src/app/shared/components/popups/popup-confirmation/popup-confirmation.component';
import { SubApplication } from '../../models/subapplications';
import { ApplicationService } from '../../services/application.service';
import { SubapplicationService } from '../../services/subapplication.service';

@Component({
  selector: 'app-subapplication',
  templateUrl: './subapplication.component.html',
  styleUrls: ['./subapplication.component.css']
})
export class SubapplicationComponent implements OnInit {

  subApplicationForm: FormGroup;
  subApplications: SubApplication[];
  subApplicationToAdd: SubApplication;
  idApplication: string;
  nameApplication: string;
  result: HandlerResponse;
  searchText!: string;
  page: number;
  pageSize: number;
  loaded: boolean;

  constructor(private formBuilder: FormBuilder, private subApplicationService: SubapplicationService, 
      private aRoute: ActivatedRoute, private applicationService: ApplicationService, public dialog: MatDialog, private ng7BootstrapBreadcrumbService: Ng7BootstrapBreadcrumbService) {
    this.subApplicationForm = this.formBuilder.group({
      id: [''],
      applicationId: [''],
      name: ['', Validators.required],
      active: [true,Validators.required]
    });
    this.aRoute.snapshot.paramMap.get('idApplication');
    this.idApplication = this.aRoute.snapshot.paramMap.get('idApplication')!;
    this.nameApplication = '';
    this.subApplications = [];
    this.loaded = false;
    this.subApplicationToAdd = new SubApplication();
    this.result = new HandlerResponse();
    this.page = 1;
    this.pageSize = 25;
  }

  ngOnInit(): void {
    this.getAllSubApplications();
    this.getApplicationById();
  }

  getAllSubApplications() {
    this.subApplicationService.getAllSubApplications(this.idApplication)
      .pipe(
        tap(() => (this.loaded = false)),
      )
      .subscribe(response => {
        this.subApplications = this.result.getData(response);
        this.loaded = true;
        console.log(response);
      });
  }

  getApplicationById() {
    this.applicationService.getApplicationById(this.idApplication)
      .subscribe(response => {
        this.nameApplication = this.result.getData(response).name;
        const breadcrumb =  {applicationName: this.nameApplication};
        this.ng7BootstrapBreadcrumbService.updateBreadcrumbLabels(breadcrumb);
      });
  }

  addSubApplication() {
    console.log(this.subApplicationForm);
    this.subApplicationToAdd.name = this.subApplicationForm.value.name;
    this.subApplicationToAdd.applicationId = this.idApplication;
    this.subApplicationToAdd.active = this.subApplicationForm.value.active;
    this.subApplicationService.addSubApplication(this.idApplication, this.subApplicationToAdd)
      .subscribe(response => {
        console.log(response);
        this.getAllSubApplications();
      });
  }

  updateSubApplication() {
    this.subApplicationToAdd.id = this.subApplicationForm.value.id;
    this.subApplicationToAdd.applicationId = this.subApplicationForm.value.applicationId;
    this.subApplicationToAdd.name = this.subApplicationForm.value.name;
    this.subApplicationToAdd.active = this.subApplicationForm.value.active;
    console.log(this.subApplicationToAdd);
    
    this.subApplicationService.updateSubApplication(this.subApplicationToAdd.applicationId,this.subApplicationToAdd.id,this.subApplicationToAdd)
      .subscribe(response => {
        console.log(response);
        this.getAllSubApplications();
      });
  }

  modifySubApplication(subapplication: SubApplication){
    this.subApplicationForm.controls['id'].setValue(subapplication.id);
    this.subApplicationForm.controls['applicationId'].setValue(subapplication.applicationId);
    this.subApplicationForm.controls['name'].setValue(subapplication.name);
    this.subApplicationForm.controls['active'].setValue(subapplication.active);
  }

  deleteSubApplication(subapplication: SubApplication){
    this.subApplicationService.deleteSubApplication(subapplication.applicationId,subapplication.id)
      .subscribe(response => {
        console.log(response);
        this.getAllSubApplications();
      });
  }

  openDialog(subapplication: SubApplication){
    const dialogRef = this.dialog.open(PopupConfirmationComponent, {
      width: '350px',
      data: "Are you sure that you want to delete this SubApplication?"
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result){
        console.log("yes");
        this.deleteSubApplication(subapplication);
      }
    })
  }

  resetFormValues(): void {
    this.subApplicationForm.reset();
    this.subApplicationForm.controls['active'].setValue(true);
  }

}
