import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApplicationComponent } from './application-component/components/application.component';
import { SubapplicationComponent } from './application-component/components/subapplication-component/subapplication.component';
import { ComponentComponent } from './application-component/components/subapplication-component/component-component/component.component';
import { ApplicationsRoutingModule } from './applications-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { ComponentTypeComponent } from './componentType-component/components/component-type.component';
import { CoreModule } from 'src/app/core/core.module';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';



@NgModule({
  declarations: [
    ApplicationComponent,
    SubapplicationComponent,
    ComponentComponent,
    ComponentTypeComponent
  ],
  imports: [
    CommonModule,
    ApplicationsRoutingModule,
    SharedModule,
    CoreModule,
    NgbPaginationModule
  ]
})
export class ApplicationsModule { }
