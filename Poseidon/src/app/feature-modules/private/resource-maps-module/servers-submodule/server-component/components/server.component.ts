import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { tap } from 'rxjs';
import { HandlerResponse } from 'src/app/core/models/handlerResponse';
import { PopupConfirmationComponent } from 'src/app/shared/components/popups/popup-confirmation/popup-confirmation.component';
import { Product } from '../../../products-submodule/product-component/models/product';
import { ProductService } from '../../../products-submodule/product-component/services/product.service';
import { Environment } from '../../environment-component/models/environment';
import { EnvironmentService } from '../../environment-component/services/environment.service';
import { Infrastructure } from '../../infrastructure-component/models/infrastructure';
import { InfrastructureService } from '../../infrastructure-component/services/infrastructure.service';
import { Server } from '../models/server';
import { ServerService } from '../services/server.service';

@Component({
  selector: 'app-server',
  templateUrl: './server.component.html',
  styleUrls: ['./server.component.css']
})
export class ServerComponent implements OnInit {

  serverForm: FormGroup;
  servers: Server[];
  products: Product[];
  environments: Environment[];
  infrastructures: Infrastructure[];
  result: HandlerResponse;
  serverToAdd: Server;
  page: number;
  pageSize: number;
  searchText!: string;
  loaded: boolean;

  constructor(private formBuilder: FormBuilder, private serverService: ServerService, private productService: ProductService,
    private environmentService: EnvironmentService, private infrastructureService: InfrastructureService, public dialog: MatDialog) {
    this.serverForm = this.formBuilder.group({
      id: [''],
      name: ['', Validators.required],
      ip: ['', Validators.required],
      location: ['', Validators.required],
      os: ['', Validators.required],
      environmentId: ['', Validators.required],
      infrastructureId: ['', Validators.required],
      productId: ['', Validators.required],
      active: [true, Validators.required]
    });
    this.servers = [];
    this.products = [];
    this.environments = [];
    this.infrastructures = [];
    this.loaded = false;
    this.result = new HandlerResponse();
    this.serverToAdd = new Server();
    this.page = 1;
    this.pageSize = 25;
  }

  ngOnInit(): void {
    this.getAllProducts();
    this.getAllEnvironments();
    this.getAllInfrastructures();
    this.getAllServers();
  }

  getAllProducts() {
    this.productService.getAllProducts()
      .subscribe(response => {
        console.log(response);
        this.products = this.result.getData(response);
      });
  }

  getAllEnvironments() {
    this.environmentService.getAllEnvironments()
      .subscribe(response => {
        this.environments = this.result.getData(response);
      });
  }

  getAllInfrastructures() {
    this.infrastructureService.getAllInfrastructures()
      .subscribe(response => {
        this.infrastructures = this.result.getData(response);
      });
  }

  getAllServers() {
    this.serverService.getAllServers()
      .pipe(
        tap(() => (this.loaded = false)),
      )
      .subscribe(response => {
        console.log(response);
        if (Array.isArray(response)) {
          this.servers = response; // La respuesta ya es un array de Server[]
        } else {
          this.servers = this.result.getData(response); // Utiliza getData si la respuesta no es un array
        }
        this.loaded = true;
      });
  }
  
  

  addServer() {
    this.serverToAdd.name = this.serverForm.value.name;
    this.serverToAdd.ip = this.serverForm.value.ip;
    this.serverToAdd.os = this.serverForm.value.os;
    this.serverToAdd.location = this.serverForm.value.location;
    this.serverToAdd.environmentId = this.serverForm.value.environmentId;
    this.serverToAdd.productId = this.serverForm.value.productId;
    this.serverToAdd.infrastructureId = this.serverForm.value.infrastructureId;
    this.serverToAdd.active = this.serverForm.value.active;
    console.log(this.serverToAdd);
    this.serverService.addServer(this.serverToAdd)
      .subscribe(response => {
        console.log(response);
        this.getAllServers();
      });
  }

  updateServer() {
    this.serverToAdd.id = this.serverForm.value.id;
    this.serverToAdd.name = this.serverForm.value.name;
    this.serverToAdd.ip = this.serverForm.value.ip;
    this.serverToAdd.os = this.serverForm.value.os;
    this.serverToAdd.location = this.serverForm.value.location;
    this.serverToAdd.environmentId = this.serverForm.value.environmentId;
    this.serverToAdd.infrastructureId = this.serverForm.value.infrastructureId;
    this.serverToAdd.productId = this.serverForm.value.productId;
    this.serverToAdd.active = this.serverForm.value.active;
    console.log(this.serverToAdd);

    this.serverService.updateServer(this.serverToAdd.id, this.serverToAdd)
      .subscribe(response => {
        console.log(response);
        this.getAllServers();
      });
  }

  deleteServer(server: Server) {
    this.serverService.deleteServer(server.id)
      .subscribe(response => {
        console.log(response);
        this.getAllServers();
      });
  }

  modifyServer(server: Server) {
    this.serverForm.controls['id'].setValue(server.id);
    this.serverForm.controls['name'].setValue(server.name);
    this.serverForm.controls['ip'].setValue(server.ip);
    this.serverForm.controls['location'].setValue(server.location);
    this.serverForm.controls['os'].setValue(server.os);
    this.serverForm.controls['productId'].setValue(server.productId);
    this.serverForm.controls['environmentId'].setValue(server.environmentId);
    this.serverForm.controls['infrastructureId'].setValue(server.infrastructureId);
    this.serverForm.controls['active'].setValue(server.active);
  }

  openDialog(server: Server): void {
    const dialogRef = this.dialog.open(PopupConfirmationComponent, {
      width: '350px',
      data: "Are you sure that you want to delete this server?"
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log("yes");
        this.deleteServer(server);
      }
    })
  }

  resetFormValues(): void {
    this.serverForm.reset();
    this.serverForm.controls['active'].setValue(true);
  }

}
