import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { VirtualListComponent } from './bigIpConfig-component/components/subcomponents/virtual-list/virtual-list.component';
import { NodeListComponent } from './bigIpConfig-component/components/subcomponents/node-list/node-list.component';
import { PoolListComponent } from './bigIpConfig-component/components/subcomponents/pool-list/pool-list.component';
import { RuleListComponent } from './bigIpConfig-component/components/subcomponents/rule-list/rule-list.component';
import { MonitorListComponent } from './bigIpConfig-component/components/subcomponents/monitor-list/monitor-list.component';

const routes: Routes = [
    { 
      path: '', 
      redirectTo: 'virtuals', 
      pathMatch: 'full',
      data: {
        breadcrumb: [
          {
            label: 'BigIpConfig',
            url: 'administration/bigipconfig'
          }
        ]
      }
    },
    { 
      path: 'virtuals', 
      component: VirtualListComponent,
      data: {
        breadcrumb: [
          {
            label: 'Home',
            url: 'administration'
          },
          {
            label: 'Administration',
            url: 'administration'
          },
          {
            label: 'BigIpConfig',
            url: 'administration/bigipconfig'
          },
          {
            label: 'Virtuals',
            url: ''
          }
        ]
      }
    },
    { 
      path: 'nodes', 
      component: NodeListComponent,
      data: {
        breadcrumb: [
          {
            label: 'Home',
            url: 'administration'
          },
          {
            label: 'Administration',
            url: 'administration'
          },
          {
            label: 'BigIpConfig',
            url: 'administration/bigipconfig'
          },
          {
            label: 'Nodes',
            url: ''
          }
        ]
      }
    },
    { 
      path: 'pools', 
      component: PoolListComponent,
      data: {
        breadcrumb: [
          {
            label: 'Home',
            url: 'administration'
          },
          {
            label: 'Administration',
            url: 'administration'
          },
          {
            label: 'BigIpConfig',
            url: 'administration/bigipconfig'
          },
          {
            label: 'Pools',
            url: ''
          }
        ]
      }
    },
    { 
      path: 'rules', 
      component: RuleListComponent,
      data: {
        breadcrumb: [
          {
            label: 'Home',
            url: 'administration'
          },
          {
            label: 'Administration',
            url: 'administration'
          },
          {
            label: 'BigIpConfig',
            url: 'administration/bigipconfig'
          },
          {
            label: 'Rules',
            url: ''
          }
        ]
      }
    },
    { 
      path: 'monitors', 
      component: MonitorListComponent,
      data: {
        breadcrumb: [
          {
            label: 'Home',
            url: 'administration'
          },
          {
            label: 'Administration',
            url: 'administration'
          },
          {
            label: 'BigIpConfig',
            url: 'administration/bigipconfig'
          },
          {
            label: 'Monitors',
            url: ''
          }
        ]
      }
    }
  ];
  

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BigIpConfigRoutingModule {}
