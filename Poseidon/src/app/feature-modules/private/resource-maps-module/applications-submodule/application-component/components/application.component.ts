import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { tap } from 'rxjs';
import { HandlerResponse } from 'src/app/core/models/handlerResponse';
import { PopupConfirmationComponent } from 'src/app/shared/components/popups/popup-confirmation/popup-confirmation.component';
import { Product } from '../../../products-submodule/product-component/models/product';
import { ProductService } from '../../../products-submodule/product-component/services/product.service';
import { Application } from '../models/application';
import { ApplicationService } from '../services/application.service';

@Component({
  selector: 'app-application',
  templateUrl: './application.component.html',
  styleUrls: ['./application.component.css']
})
export class ApplicationComponent implements OnInit {

  applicationForm: FormGroup;
  products: Product[];
  applications: Application[];
  applicationToAdd: Application;
  result: HandlerResponse;
  searchText!: string;
  page: number;
  pageSize: number;
  loaded: boolean;

  constructor(private formBuilder: FormBuilder, private productService: ProductService, private applicationService: ApplicationService, public dialog: MatDialog) {
    this.applicationForm = this.formBuilder.group({
      id: [''],
      productName: ['', Validators.required],
      name: ['', Validators.required],
      active: [true,Validators.required]
    });
    this.loaded = false;
    this.products = [];
    this.applications = [];
    this.applicationToAdd = new Application();
    this.result = new HandlerResponse();
    this.page = 1;
    this.pageSize = 25;
  }

  ngOnInit(): void {
    this.getAllProducts();
    this.getAllApplications();
  }

  getAllProducts() {
    this.productService.getAllProducts()
      .subscribe(response => {
        console.log(response);
        this.products = this.result.getData(response);
      });
  }

  addApplication() {
    console.log(this.applicationForm);
    this.applicationToAdd.name = this.applicationForm.value.name;
    this.applicationToAdd.productId = this.applicationForm.value.productName;
    this.applicationToAdd.active = this.applicationForm.value.active;
    this.applicationService.addApplication(this.applicationToAdd)
      .subscribe(response => {
        console.log(response);
        this.getAllApplications();
      });
  }

  getAllApplications() {
    this.applicationService.getAllApplications()
      .pipe(
        tap(() => (this.loaded = false)),
      )
      .subscribe(response => {
        console.log(response);
        if (Array.isArray(response)) {
          this.applications = response; // La respuesta ya es un array de Application[]
        } else {
          this.applications = this.result.getData(response); // Utiliza getData si la respuesta no es un array
        }
        this.loaded = true;
      });
  }
  

  updateApplication() {
    this.applicationToAdd.id = this.applicationForm.value.id;
    this.applicationToAdd.productId = this.applicationForm.value.productName;
    this.applicationToAdd.name = this.applicationForm.value.name;
    this.applicationToAdd.active = this.applicationForm.value.active;
    console.log(this.applicationToAdd);

    this.applicationService.updateApplication(this.applicationToAdd.id,this.applicationToAdd)
      .subscribe(response => {
        console.log(response);
        this.getAllApplications();
      });
  }

  modifyApplication(application: Application) {
    this.applicationForm.controls['id'].setValue(application.id);
    this.applicationForm.controls['productName'].setValue(application.productId);
    this.applicationForm.controls['name'].setValue(application.name);
    this.applicationForm.controls['active'].setValue(application.active);
  }

  deleteApplication(application: Application) {
    this.applicationService.deteleApplication(application.id)
      .subscribe(response => {
        console.log(response);
        this.getAllApplications();
      });
  }

  openDialog(application: Application): void{
    const dialogRef = this.dialog.open(PopupConfirmationComponent, {
      width: '350px',
      data: "Are you sure that you want to delete this application?"
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result){
        console.log("yes");
        this.deleteApplication(application);
      }
    })
  }

  resetFormValues(): void {
    this.applicationForm.reset();
    this.applicationForm.controls['active'].setValue(true);
  }

}
