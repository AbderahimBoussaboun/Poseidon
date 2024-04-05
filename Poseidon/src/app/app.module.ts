import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ApplicationsModule } from './feature-modules/private/resource-maps-module/applications-submodule/applications.module';
import { ProductsModule } from './feature-modules/private/resource-maps-module/products-submodule/products.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { SharedModule } from './shared/shared.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BalancersModule } from './feature-modules/private/resource-maps-module/balancers-submodule/balancers.module';
import { ServersModule } from './feature-modules/private/resource-maps-module/servers-submodule/servers.module';
import { CoreModule } from './core/core.module';
//import { NgDynamicBreadcrumbModule } from 'ng-dynamic-breadcrumb';
import { Ng7BootstrapBreadcrumbModule } from 'ng7-bootstrap-breadcrumb';
import { SpinnerComponent } from './shared/components/spinner/spinner.component';
import { Interceptor } from './core/interceptors/interceptor';

@NgModule({
    declarations: [
        AppComponent
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: Interceptor, multi: true }
    ],
    bootstrap: [AppComponent],
    imports: [
        BrowserModule,
        AppRoutingModule,
        ApplicationsModule,
        ProductsModule,
        SharedModule,
        HttpClientModule,
        BrowserAnimationsModule,
        BalancersModule,
        ServersModule,
        CoreModule,
        Ng7BootstrapBreadcrumbModule
    ]
})
export class AppModule { }
