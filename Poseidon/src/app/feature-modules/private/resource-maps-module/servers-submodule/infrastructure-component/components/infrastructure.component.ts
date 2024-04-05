import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { tap } from 'rxjs';
import { HandlerResponse } from 'src/app/core/models/handlerResponse';
import { PopupConfirmationComponent } from 'src/app/shared/components/popups/popup-confirmation/popup-confirmation.component';
import { Infrastructure } from '../models/infrastructure';
import { InfrastructureService } from '../services/infrastructure.service';

@Component({
  selector: 'app-infrastructure',
  templateUrl: './infrastructure.component.html',
  styleUrls: ['./infrastructure.component.css']
})
export class InfrastructureComponent implements OnInit {

  infrastructureForm: FormGroup;
  infrastructures: Infrastructure[];
  infrastructureToAdd: Infrastructure;
  result: HandlerResponse;
  page: number;
  pageSize: number;
  searchText!: string;
  loaded: boolean;

  constructor(private formBuilder: FormBuilder, private infrastructureService: InfrastructureService, public dialog: MatDialog) {
    this.infrastructureForm = this.formBuilder.group({
      id: [''],
      name: ['', Validators.required],
      active: [true, Validators.required]
    });
    this.infrastructures = [];
    this.loaded = false;
    this.infrastructureToAdd = new Infrastructure();
    this.result = new HandlerResponse();
    this.page = 1;
    this.pageSize = 25;
  }

  ngOnInit(): void {
    this.getAllInfrastructures();
  }

  getAllInfrastructures(){
    this.infrastructureService.getAllInfrastructures()
      .pipe(
        tap(() => (this.loaded = false)),
      )
      .subscribe(response => {
        this.infrastructures = this.result.getData(response);
        this.loaded = true;
      });
  }

  addInfrastructure(){
    console.log(this.infrastructureForm);
    this.infrastructureToAdd.name = this.infrastructureForm.value.name;
    this.infrastructureToAdd.active = this.infrastructureForm.value.active;
    this.infrastructureService.addInfrastructure(this.infrastructureToAdd)
      .subscribe(response => {
        console.log(response);
        this.getAllInfrastructures();
      });
  }

  deleteInfrastructure(infrastructure: Infrastructure){
    this.infrastructureService.deleteInfrastructure(infrastructure.id)
      .subscribe(response => {
        console.log(response);
        this.getAllInfrastructures();
      });
  }

  updateInfrastructure(){
    this.infrastructureToAdd.id = this.infrastructureForm.value.id;
    this.infrastructureToAdd.name = this.infrastructureForm.value.name;
    this.infrastructureToAdd.active = this.infrastructureForm.value.active;
    console.log(this.infrastructureToAdd);

    this.infrastructureService.updateInfrastructure(this.infrastructureToAdd.id,this.infrastructureToAdd)
      .subscribe(response => {
        console.log(response);
        this.getAllInfrastructures();
      });
  }

  modifyInfrastructure(infrastructure: Infrastructure){
    this.infrastructureForm.controls['id'].setValue(infrastructure.id);
    this.infrastructureForm.controls['name'].setValue(infrastructure.name);
    this.infrastructureForm.controls['active'].setValue(infrastructure.active);
  }

  openDialog(infrastructure: Infrastructure): void{
    const dialogRef = this.dialog.open(PopupConfirmationComponent, {
      width: '350px',
      data: "Are you sure that you want to delete this infrastructure?"
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result){
        console.log("yes");
        this.deleteInfrastructure(infrastructure);
      }
    })
  }

  resetFormValues(): void {
    this.infrastructureForm.reset();
    this.infrastructureForm.controls['active'].setValue(true);
  }

}
