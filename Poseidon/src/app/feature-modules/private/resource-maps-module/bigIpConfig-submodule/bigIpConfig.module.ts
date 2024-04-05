import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { BigIpConfigRoutingModule } from './bigIpConfig-routing.module';
import { BigIpConfigComponent } from './bigIpConfig-component/components/bigIpConfig.component';
import { NodeListComponent } from './bigIpConfig-component/components/subcomponents/node-list/node-list.component';
import { VirtualListComponent } from './bigIpConfig-component/components/subcomponents/virtual-list/virtual-list.component';
import { PoolListComponent } from './bigIpConfig-component/components/subcomponents/pool-list/pool-list.component';
import { RuleListComponent } from './bigIpConfig-component/components/subcomponents/rule-list/rule-list.component';
import { MonitorListComponent } from './bigIpConfig-component/components/subcomponents/monitor-list/monitor-list.component';
import {  HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { NavbarComponent } from './bigIpConfig-component/components/subcomponents/navbar/navbar.component';
import { NavbarModalComponent } from './bigIpConfig-component/components/subcomponents/navbar/modal/modal.component';

@NgModule({
  declarations: [
    BigIpConfigComponent,
    NodeListComponent,
    VirtualListComponent,
    PoolListComponent,
    RuleListComponent,
    MonitorListComponent,
    NavbarComponent,
    NavbarModalComponent,
    
  ],
  imports: [
    BigIpConfigRoutingModule, HttpClientModule, CommonModule, SharedModule
  ],
  providers: [],
  bootstrap: [BigIpConfigComponent]
})
export class BigIpConfigModule { }
