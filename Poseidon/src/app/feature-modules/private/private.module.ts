import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PrivateHomeComponent } from './components/home/private-home.component';
import { RouterModule } from '@angular/router';



@NgModule({
    declarations: [
        PrivateHomeComponent,
    ],
    imports: [
        CommonModule,
        RouterModule
    ]
})
export class PrivateModule { }
