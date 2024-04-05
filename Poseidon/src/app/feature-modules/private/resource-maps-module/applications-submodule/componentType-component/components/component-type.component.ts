import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { tap } from 'rxjs';
import { HandlerResponse } from 'src/app/core/models/handlerResponse';
import { PopupConfirmationComponent } from 'src/app/shared/components/popups/popup-confirmation/popup-confirmation.component';
import { ComponentType } from '../models/componentType';
import { ComponentTypeService } from '../services/component-type.service';

@Component({
  selector: 'app-component-type',
  templateUrl: './component-type.component.html',
  styleUrls: ['./component-type.component.css']
})
export class ComponentTypeComponent implements OnInit {

  componentTypeForm: FormGroup;
  componentsType: ComponentType[];
  componentTypeToAdd: ComponentType;
  result: HandlerResponse;
  searchText!: string;
  page: number;
  pageSize: number;
  loaded: boolean;

  constructor(private formBuilder: FormBuilder, private componentTypeService: ComponentTypeService, public dialog: MatDialog) {
    this.componentTypeForm = this.formBuilder.group({
      id: [''],
      name: ['',Validators.required],
      active: [true, Validators.required]
    });
    this.componentsType = [];
    this.componentTypeToAdd = new ComponentType();
    this.result = new HandlerResponse();
    this.loaded = false;
    this.page = 1;
    this.pageSize = 25;
  }

  ngOnInit(): void {
    this.getAllComponentTypes();
  }

  getAllComponentTypes(){
    this.componentTypeService.getAllComponentTypes()
      .pipe(
        tap(() => (this.loaded = false)),
      )
      .subscribe(response => {
        console.log(response);
        this.componentsType = this.result.getData(response);
        this.loaded = true;
      });
  }

  addComponentType(){
    console.log(this.componentTypeForm);
    this.componentTypeToAdd.name = this.componentTypeForm.value.name;
    this.componentTypeToAdd.active = this.componentTypeForm.value.active;
    console.log(this.componentTypeToAdd);
    this.componentTypeService.addComponentType(this.componentTypeToAdd)
      .subscribe(response => {
        console.log(response);
        this.getAllComponentTypes();
      });
  }

  updateComponentType(){
    this.componentTypeToAdd.id = this.componentTypeForm.value.id;
    this.componentTypeToAdd.name = this.componentTypeForm.value.name;
    this.componentTypeToAdd.active = this.componentTypeForm.value.active;
    console.log(this.componentTypeToAdd);

    this.componentTypeService.updateComponentType(this.componentTypeToAdd.id,this.componentTypeToAdd)
      .subscribe(response => {
        console.log(response);
        this.getAllComponentTypes();
      });
  }

  modifyComponentType(componentType: ComponentType){
    this.componentTypeForm.controls['id'].setValue(componentType.id);
    this.componentTypeForm.controls['name'].setValue(componentType.name);
    this.componentTypeForm.controls['active'].setValue(componentType.active);
  }

  deleteComponentType(componentType: ComponentType){
    this.componentTypeService.deleteComponentType(componentType.id)
      .subscribe(response => {
        console.log(response);
        this.getAllComponentTypes();
      });
  }

  openDialog(componentType: ComponentType): void{
    const dialogRef = this.dialog.open(PopupConfirmationComponent, {
      width: '350px',
      data: "Are you sure that you want to delete this ComponentType?"
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result){
        console.log("yes");
        this.deleteComponentType(componentType);
      }
    })
  }

  resetFormValues(): void {
    this.componentTypeForm.reset();
    this.componentTypeForm.controls['active'].setValue(true);
  }

}
