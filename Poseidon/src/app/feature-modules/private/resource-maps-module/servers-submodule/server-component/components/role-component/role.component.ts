import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { HandlerResponse } from 'src/app/core/models/handlerResponse';
import { Role } from '../../models/role';
import { ServerService } from '../../services/server.service';
import { PopupConfirmationComponent } from 'src/app/shared/components/popups/popup-confirmation/popup-confirmation.component';
import { RoleService } from '../../services/role.service';
import { tap } from 'rxjs';
import { Ng7BootstrapBreadcrumbService } from 'ng7-bootstrap-breadcrumb';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css']
})
export class RoleComponent implements OnInit {

  roleForm: FormGroup;
  roles: Role[];
  roleToAdd: Role;
  idServer: string;
  nameServer: string;
  result: HandlerResponse;
  searchText!: string;
  page: number;
  pageSize: number;
  loaded: boolean;

  constructor(private formBuilder: FormBuilder, private roleService: RoleService, 
      private aRoute: ActivatedRoute, private serverService: ServerService, public dialog: MatDialog, private ng7BootstrapBreadcrumbService: Ng7BootstrapBreadcrumbService) {
    this.roleForm = this.formBuilder.group({
      id: [''],
      serverId: [''],
      name: ['', Validators.required],
      type: ['',Validators.required],
      active: [true,Validators.required]
    });
    this.aRoute.snapshot.paramMap.get('idServer');
    this.idServer = this.aRoute.snapshot.paramMap.get('idServer')!;
    this.nameServer = '';
    this.roles = [];
    this.roleToAdd = new Role();
    this.result = new HandlerResponse();
    this.loaded = false;
    this.page = 1;
    this.pageSize = 25;
  }

  ngOnInit(): void {
    this.getAllRoles();
    this.getServerById();
  }

  getAllRoles() {
    this.roleService.getAllRoles(this.idServer)
      .pipe(
        tap(() => (this.loaded = false)),
      )
      .subscribe(response => {
        console.log(response);
        this.roles = this.result.getData(response);
        this.loaded = true;
      });
  }

  getServerById() {
    this.serverService.getServerById(this.idServer)
      .subscribe(response => {
        this.nameServer = this.result.getData(response).name;
        const breadcrumb =  {serverName: this.nameServer};
        this.ng7BootstrapBreadcrumbService.updateBreadcrumbLabels(breadcrumb);
      });
  }

  addRole() {
    console.log(this.roleForm);
    this.roleToAdd.name = this.roleForm.value.name;
    this.roleToAdd.serverId = this.idServer;
    var type = Number(this.roleForm.value.type);
    this.roleToAdd.type = type;
    this.roleToAdd.active = this.roleForm.value.active;
    console.log(this.roleToAdd);
    this.roleService.addRole(this.idServer, this.roleToAdd)
      .subscribe(response => {
        console.log(response);
        this.getAllRoles();
      });
  }

  updateRole() {
    this.roleToAdd.id = this.roleForm.value.id;
    this.roleToAdd.serverId = this.roleForm.value.serverId;
    this.roleToAdd.name = this.roleForm.value.name;
    this.roleToAdd.type = this.roleForm.value.type;
    this.roleToAdd.active = this.roleForm.value.active;
    console.log(this.roleToAdd);
    
    this.roleService.updateRole(this.roleToAdd.serverId, this.roleToAdd.id, this.roleToAdd)
      .subscribe(response => {
        console.log(response);
        this.getAllRoles();
      })
  }

  modifyRole(role: Role){
    this.roleForm.controls['id'].setValue(role.id);
    this.roleForm.controls['serverId'].setValue(role.serverId);
    this.roleForm.controls['name'].setValue(role.name);
    this.roleForm.controls['type'].setValue(role.type);
    this.roleForm.controls['active'].setValue(role.active);
  }

  deleteRole(role: Role){
    this.roleService.deleteRole(role.serverId,role.id)
      .subscribe(response => {
        console.log(response);
        this.getAllRoles();
      });
  }

  openDialog(role: Role){
    const dialogRef = this.dialog.open(PopupConfirmationComponent, {
      width: '350px',
      data: "Are you sure that you want to delete this Role?"
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result){
        console.log("yes");
        this.deleteRole(role);
      }
    })
  }

  resetFormValues(): void {
    this.roleForm.reset();
    this.roleForm.controls['active'].setValue(true);
  }

}
