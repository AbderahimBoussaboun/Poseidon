import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PrivateHomeComponent } from './feature-modules/private/components/home/private-home.component';
import { ApplicationComponent } from './feature-modules/private/resource-maps-module/applications-submodule/application-component/components/application.component';
import { ComponentComponent } from './feature-modules/private/resource-maps-module/applications-submodule/application-component/components/subapplication-component/component-component/component.component';
import { SubapplicationComponent } from './feature-modules/private/resource-maps-module/applications-submodule/application-component/components/subapplication-component/subapplication.component';
import { ComponentTypeComponent } from './feature-modules/private/resource-maps-module/applications-submodule/componentType-component/components/component-type.component';
import { BalancerComponent } from './feature-modules/private/resource-maps-module/balancers-submodule/balancer-component/components/balancer.component';
import { ProductComponent } from './feature-modules/private/resource-maps-module/products-submodule/product-component/components/product.component';
import { EnvironmentComponent } from './feature-modules/private/resource-maps-module/servers-submodule/environment-component/components/environment.component';
import { InfrastructureComponent } from './feature-modules/private/resource-maps-module/servers-submodule/infrastructure-component/components/infrastructure.component';
import { RoleComponent } from './feature-modules/private/resource-maps-module/servers-submodule/server-component/components/role-component/role.component';
import { ServerApplicationComponent } from './feature-modules/private/resource-maps-module/servers-submodule/server-component/components/role-component/serverApplication-component/server-application.component';
import { ServerComponent } from './feature-modules/private/resource-maps-module/servers-submodule/server-component/components/server.component';
import { BigIpConfigComponent } from './feature-modules/private/resource-maps-module/bigIpConfig-submodule/bigIpConfig-component/components/bigIpConfig.component';
import { MonitorListComponent } from './feature-modules/private/resource-maps-module/bigIpConfig-submodule/bigIpConfig-component/components/subcomponents/monitor-list/monitor-list.component';
import { VirtualListComponent } from './feature-modules/private/resource-maps-module/bigIpConfig-submodule/bigIpConfig-component/components/subcomponents/virtual-list/virtual-list.component';
import { NodeListComponent } from './feature-modules/private/resource-maps-module/bigIpConfig-submodule/bigIpConfig-component/components/subcomponents/node-list/node-list.component';
import { PoolListComponent } from './feature-modules/private/resource-maps-module/bigIpConfig-submodule/bigIpConfig-component/components/subcomponents/pool-list/pool-list.component';
import { RuleListComponent } from './feature-modules/private/resource-maps-module/bigIpConfig-submodule/bigIpConfig-component/components/subcomponents/rule-list/rule-list.component';
import { PublicHomeComponent } from './feature-modules/public/components/home/public-home.component';

const routes: Routes = [
  {
    path: '',
    component: PublicHomeComponent,
    loadChildren: () => import('./feature-modules/public/public.module').then(m => m.PublicModule),
    data: {
      breadcrumb: [
        {
          label: 'Home',
          url: ''
        }
      ]
    },
  },
  {
    path: 'administration',
    component: PrivateHomeComponent,
    loadChildren: () => import('./feature-modules/private/private.module').then(m => m.PrivateModule),
    data: {
      breadcrumb: [
        {
          label: 'Home',
          url: '/'
        },
        {
          label: 'Administration',
          url: ''
        }
      ]
    },
  },
  {
    path: 'administration/products',
    component: ProductComponent,
    loadChildren: () => import('./feature-modules/private/resource-maps-module/products-submodule/products.module').then(m => m.ProductsModule),
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
          label: 'Products',
          url: ''
        }
      ]
    },
  },
  {
    path: 'administration/applications',
    component: ApplicationComponent,
    loadChildren: () => import('./feature-modules/private/resource-maps-module/applications-submodule/applications.module').then(m => m.ApplicationsModule),
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
          label: 'Applications',
          url: ''
        }
      ]
    },
  },
  {
    path: 'administration/applications/:idApplication/subapplications',
    component: SubapplicationComponent,
    loadChildren: () => import('./feature-modules/private/resource-maps-module/applications-submodule/applications.module').then(m => m.ApplicationsModule),
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
          label: 'Applications',
          url: 'administration/applications'
        },
        {
          label: '{{applicationName}}',
          url: ''
        }
      ]
    },
  },
  {
    path: 'administration/applications/:idApplication/subapplications/:idSubApplication/components',
    component: ComponentComponent,
    loadChildren: () => import('./feature-modules/private/resource-maps-module/applications-submodule/applications.module').then(m => m.ApplicationsModule),
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
          label: 'Applications',
          url: 'administration/applications'
        },
        {
          label: '{{applicationName}}',
          url: 'administration/applications/:idApplication/subapplications'
        },
        {
          label: '{{subapplicationName}}',
          url: ''
        }
      ]
    },
  },
  {
    path: 'administration/balancers',
    component: BalancerComponent,
    loadChildren: () => import('./feature-modules/private/resource-maps-module/balancers-submodule/balancers.module').then(m => m.BalancersModule),
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
          label: 'Balancers',
          url: ''
        }
      ]
    },
  },
  {
    path: 'administration/componentTypes',
    component: ComponentTypeComponent,
    loadChildren: () => import('./feature-modules/private/resource-maps-module/applications-submodule/applications.module').then(m => m.ApplicationsModule),
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
          label: 'Component Types',
          url: ''
        }
      ]
    },
  },
  {
    path: 'administration/environments',
    component: EnvironmentComponent,
    loadChildren: () => import('./feature-modules/private/resource-maps-module/servers-submodule/servers.module').then(m => m.ServersModule),
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
          label: 'Environments',
          url: ''
        }
      ]
    },
  },
  {
    path: 'administration/servers',
    component: ServerComponent,
    loadChildren: () => import('./feature-modules/private/resource-maps-module/servers-submodule/servers.module').then(m => m.ServersModule),
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
          label: 'Servers',
          url: ''
        }
      ]
    },
  },
  {
    path: 'administration/servers/:idServer/roles',
    component: RoleComponent,
    loadChildren: () => import('./feature-modules/private/resource-maps-module/servers-submodule/servers.module').then(m => m.ServersModule),
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
          label: 'Servers',
          url: 'administration/servers'
        },
        {
          label: '{{serverName}}',
          url: ''
        }
      ]
    },
  },
  {
    path: 'administration/servers/:idServer/roles/:idRole/serverApplications',
    component: ServerApplicationComponent,
    loadChildren: () => import('./feature-modules/private/resource-maps-module/servers-submodule/servers.module').then(m => m.ServersModule),
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
          label: 'Servers',
          url: 'administration/servers'
        },
        {
          label: '{{serverName}}',
          url: 'administration/servers/:idServer/roles'
        },
        {
          label: '{{roleName}}',
          url: ''
        }
      ]
    },
  },
  {
    path: 'administration/infrastructures',
    component: InfrastructureComponent,
    loadChildren: () => import('./feature-modules/private/resource-maps-module/servers-submodule/servers.module').then(m => m.ServersModule),
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
          label: 'Infrastructures',
          url: ''
        }
      ]
    },
  },
  {
    path: 'administration/bigipconfig',
    component: BigIpConfigComponent,
    loadChildren: () => import('./feature-modules/private/resource-maps-module/bigIpConfig-submodule/bigIpConfig.module').then(m => m.BigIpConfigModule),
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
          url: ''
        }
      ]
    },
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
