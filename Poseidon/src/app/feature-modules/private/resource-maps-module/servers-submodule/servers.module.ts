import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ServerComponent } from './server-component/components/server.component';
import { RoleComponent } from './server-component/components/role-component/role.component';
import { ServerApplicationComponent } from './server-component/components/role-component/serverApplication-component/server-application.component';
import { EnvironmentComponent } from './environment-component/components/environment.component';
import { ServersRoutingModule } from './servers-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { InfrastructureComponent } from './infrastructure-component/components/infrastructure.component';
import { CoreModule } from 'src/app/core/core.module';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';



@NgModule({
  declarations: [
    ServerComponent,
    RoleComponent,
    ServerApplicationComponent,
    EnvironmentComponent,
    InfrastructureComponent
  ],
  imports: [
    CommonModule,
    ServersRoutingModule,
    SharedModule,
    CoreModule,
    NgbPaginationModule
  ]
})
export class ServersModule { }
