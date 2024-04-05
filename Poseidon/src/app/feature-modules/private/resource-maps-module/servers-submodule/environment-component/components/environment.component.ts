import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { tap } from 'rxjs';
import { HandlerResponse } from 'src/app/core/models/handlerResponse';
import { PopupConfirmationComponent } from 'src/app/shared/components/popups/popup-confirmation/popup-confirmation.component';
import { Environment } from '../models/environment';
import { EnvironmentService } from '../services/environment.service';

@Component({
  selector: 'app-environment',
  templateUrl: './environment.component.html',
  styleUrls: ['./environment.component.css']
})
export class EnvironmentComponent implements OnInit {

  environmentForm: FormGroup;
  environments: Environment[];
  environmentToAdd: Environment;
  result: HandlerResponse;
  page: number;
  pageSize: number;
  searchText!: string;
  loaded: boolean;

  constructor(private formBuilder: FormBuilder, private environmentService: EnvironmentService, public dialog: MatDialog) {
    this.environmentForm = this.formBuilder.group({
      id: [''],
      name: ['', Validators.required],
      active: [true, Validators.required]
    });
    this.environments = [];
    this.environmentToAdd = new Environment();
    this.result = new HandlerResponse();
    this.loaded = false;
    this.page = 1;
    this.pageSize = 25;
  }

  ngOnInit(): void {
    this.getAllEnvironments();
  }

  getAllEnvironments(){
    this.environmentService.getAllEnvironments()
      .pipe(
        tap(() => (this.loaded = false)),
      )
      .subscribe(response => {
        this.environments = this.result.getData(response);
        this.loaded = true;
      });
  }

  addEnvironment(){
    console.log(this.environmentForm);
    this.environmentToAdd.name = this.environmentForm.value.name;
    this.environmentToAdd.active = this.environmentForm.value.active;
    this.environmentService.addEnvironment(this.environmentToAdd)
      .subscribe(response => {
        console.log(response);
        this.getAllEnvironments();
      });
  }

  deleteEnvironment(environment: Environment){
    this.environmentService.deleteEnvironment(environment.id)
      .subscribe(response => {
        console.log(response);
        this.getAllEnvironments();
      });
  }

  updateEnvironment(){
    this.environmentToAdd.id = this.environmentForm.value.id;
    this.environmentToAdd.name = this.environmentForm.value.name;
    this.environmentToAdd.active = this.environmentForm.value.active;
    console.log(this.environmentToAdd);

    this.environmentService.updateEnvironment(this.environmentToAdd.id,this.environmentToAdd)
      .subscribe(response => {
        console.log(response);
        this.getAllEnvironments();
      });
  }

  modifyEnvironment(environment: Environment){
    this.environmentForm.controls['id'].setValue(environment.id);
    this.environmentForm.controls['name'].setValue(environment.name);
    this.environmentForm.controls['active'].setValue(environment.active);
  }

  openDialog(environment: Environment): void{
    const dialogRef = this.dialog.open(PopupConfirmationComponent, {
      width: '350px',
      data: "Are you sure that you want to delete this environment?"
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result){
        console.log("yes");
        this.deleteEnvironment(environment);
      }
    })
  }

  resetFormValues(): void {
    this.environmentForm.reset();
    this.environmentForm.controls['active'].setValue(true);
  }

}
