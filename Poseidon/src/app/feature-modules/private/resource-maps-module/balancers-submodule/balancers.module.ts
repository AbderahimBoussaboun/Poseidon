import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BalancersRoutingModule } from './balancers-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { BalancerComponent } from './balancer-component/components/balancer.component';
import { CoreModule } from 'src/app/core/core.module';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';



@NgModule({
  declarations: [
    BalancerComponent
  ],
  imports: [
    CommonModule,
    BalancersRoutingModule,
    SharedModule,
    CoreModule,
    NgbPaginationModule
  ]
})
export class BalancersModule { }
