import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { tap } from 'rxjs';
import { HandlerResponse } from 'src/app/core/models/handlerResponse';
import { PopupConfirmationComponent } from 'src/app/shared/components/popups/popup-confirmation/popup-confirmation.component';
import { Balancer } from '../models/balancer';
import { BalancerService } from '../services/balancer.service';

@Component({
  selector: 'app-components',
  templateUrl: './balancer.component.html',
  styleUrls: ['./balancer.component.css']
})
export class BalancerComponent implements OnInit {

  balancerForm: FormGroup;
  balancers: Balancer[];
  balancerToAdd: Balancer;
  result: HandlerResponse;
  searchText!: string;
  page: number;
  pageSize: number;
  loaded: boolean;

  constructor(private formBuilder: FormBuilder, private balancerService: BalancerService, public dialog: MatDialog) {
    this.balancerForm = this.formBuilder.group({
      id: [''],
      ip: ['', Validators.required],
      ports: ['', Validators.required],
      name: ['', Validators.required],
      active: [true, Validators.required]
    });
    this.balancers = [];
    this.balancerToAdd = new Balancer();
    this.result = new HandlerResponse();
    this.loaded = false;
    this.page = 1;
    this.pageSize = 25;
  }

  ngOnInit(): void {
    this.getAllBalancers();
  }

  getAllBalancers() {
    this.balancerService.getAllBalancers()
      .pipe(
        tap(() => (this.loaded = false)),
      )
      .subscribe(response => {
        console.log(response);
        if (Array.isArray(response)) {
          this.balancers = response; // La respuesta ya es un array de Balancer[]
        } else {
          this.balancers = this.result.getData(response); // Utiliza getData si la respuesta no es un array
        }
        this.loaded = true;
      });
  }
  

  addBalancer() {
    console.log(this.balancerForm);
    this.balancerToAdd.ip = this.balancerForm.value.ip;
    this.balancerToAdd.name = this.balancerForm.value.name;
    console.log(this.balancerForm.value.ports);
    var portsSplitted = this.balancerForm.value.ports.split(",");
    this.balancerToAdd.ports = portsSplitted;
    this.balancerToAdd.active = this.balancerForm.value.active;
    this.balancerService.addBalancer(this.balancerToAdd)
      .subscribe(response => {
        console.log(response);
        this.getAllBalancers();
      });
  }

  updateBalancer(){
    this.balancerToAdd.id = this.balancerForm.value.id;
    this.balancerToAdd.ip = this.balancerForm.value.ip;
    this.balancerToAdd.name = this.balancerForm.value.name;
    var portsFormValue = this.balancerForm.value.ports;
    var portsSplitted = typeof portsFormValue === 'string' ? portsFormValue.split(",") : portsFormValue;
    this.balancerToAdd.ports = portsSplitted;
    console.log(this.balancerToAdd);
    this.balancerService.updateBalancer(this.balancerToAdd.id,this.balancerToAdd)
      .subscribe(response => {
        console.log(response);
        this.getAllBalancers();
      });
  }

  modifyBalancer(balancer: Balancer){
    this.balancerForm.controls['id'].setValue(balancer.id);
    this.balancerForm.controls['ip'].setValue(balancer.ip);
    this.balancerForm.controls['name'].setValue(balancer.name);
    this.balancerForm.controls['ports'].setValue(balancer.ports);
  }

  deleteBalancer(balancer: Balancer){
    this.balancerService.deleteBalancer(balancer.id)
      .subscribe(response => {
        console.log(response);
        this.getAllBalancers();
      });
  }

  openDialog(balancer: Balancer): void{
    const dialogRef = this.dialog.open(PopupConfirmationComponent, {
      width: '350px',
      data: "Are you sure that you want to delete this balancer?"
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result){
        console.log("yes");
        this.deleteBalancer(balancer);
      }
    })
  }

  resetFormValues(): void {
    this.balancerForm.reset();
    this.balancerForm.controls['active'].setValue(true);
  }

}
