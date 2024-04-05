import { Component, OnInit } from '@angular/core';
import { Product } from '../models/product';
import { ProductService } from '../services/product.service';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms'
import { MatDialog } from '@angular/material/dialog';
import { PopupConfirmationComponent } from 'src/app/shared/components/popups/popup-confirmation/popup-confirmation.component';
import { HandlerResponse } from 'src/app/core/models/handlerResponse';
import { tap } from 'rxjs';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  productForm: FormGroup;
  products: Product[];
  result: HandlerResponse;
  productToAdd: Product = new Product();
  searchText!: string;
  page: number;
  pageSize: number;
  loaded: boolean;
  constructor(private formBuilder: FormBuilder, private productService: ProductService, public dialog: MatDialog) {
    this.productForm = this.formBuilder.group({
      id: [''],
      name: ['', Validators.required],
      active: [true, Validators.required]
    });
    this.products = [];
    this.result = new HandlerResponse();
    this.loaded = false;
    this.page = 1;
    this.pageSize = 25;
  }

  ngOnInit(): void {
    this.getAllProducts();
  }

  getAllProducts() {
    this.productService.getAllProducts()
      .pipe(
        tap(() => (this.loaded = false)),
      )
      .subscribe(response => {
        // TO DO
        // IF STATUS CODE EQUALS 404 -> POPUP
        console.log(response);
        this.products = this.result.getData(response);
        this.loaded = true;
        console.log(this.result.getData(response));
        console.log(this.result.getStatusCode(response));
      });
  }

  addProduct() {
    console.log(this.productForm);
    this.productToAdd.name = this.productForm.value.name;
    this.productToAdd.active = this.productForm.value.active;
    this.productService.addProduct(this.productToAdd)
      .subscribe(response => {
        // TO DO
        // IF STATUS CODE EQUALS 404 -> POPUP
        console.log(response);
        this.getAllProducts();
      });
  }

  deleteProduct(product: Product) {
    this.productService.deleteProduct(product.id)
      .subscribe(response => {
        // TO DO
        // IF STATUS CODE EQUALS 404 -> POPUP
        console.log(response);
        this.getAllProducts();
      });
  }

  updateProduct() {
    this.productToAdd.id = this.productForm.value.id;
    this.productToAdd.name = this.productForm.value.name;
    this.productToAdd.active = this.productForm.value.active;
    console.log(this.productToAdd);

    this.productService.updateProduct(this.productToAdd.id, this.productToAdd)
      .subscribe(response => {
        // TO DO
        // IF STATUS CODE EQUALS 404 -> POPUP
        console.log(response);
        this.getAllProducts();
      });
  }

  modifyProduct(product: Product) {
    this.productForm.controls['id'].setValue(product.id);
    this.productForm.controls['name'].setValue(product.name);
    this.productForm.controls['active'].setValue(product.active);
  }

  openDialog(product: Product): void {
    const dialogRef = this.dialog.open(PopupConfirmationComponent, {
      width: '350px',
      data: "Are you sure that you want to delete this product?"
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log("yes");
        this.deleteProduct(product);
      }
    })
  }

  resetFormValues(): void {
    this.productForm.reset();
    this.productForm.controls['active'].setValue(true);
  }

}
