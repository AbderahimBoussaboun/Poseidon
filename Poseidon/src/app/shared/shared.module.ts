import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FooterComponent } from './layouts/footer/footer.component';
import { HeaderComponent } from './layouts/header/header.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { PopupConfirmationComponent } from './components/popups/popup-confirmation/popup-confirmation.component';
import { RouterModule } from '@angular/router';
import { NgxPaginationModule } from 'ngx-pagination';
import { SearchFilterPipe } from './components/pipes/search-filter.pipe';
import { SortDirective } from './components/directives/sort.directive';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { ToastsContainer } from './components/toasts/toasts-container.component';


@NgModule({
  declarations: [
    FooterComponent,
    HeaderComponent,
    PopupConfirmationComponent,
    SearchFilterPipe,
    SortDirective,
    SpinnerComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatButtonModule,
    RouterModule,
    NgxPaginationModule,
    FormsModule,
    ToastsContainer
  ],
  exports: [
    FooterComponent,
    HeaderComponent,
    ReactiveFormsModule,
    NgxPaginationModule,
    SearchFilterPipe,
    FormsModule,
    SortDirective,
    SpinnerComponent,
    ToastsContainer
  ]
})
export class SharedModule { }
