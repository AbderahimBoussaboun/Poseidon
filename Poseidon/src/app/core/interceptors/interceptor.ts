import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
  HttpResponse
} from '@angular/common/http';
import { catchError, finalize, Observable, tap, throwError } from 'rxjs';
import { SpinnerService } from '../../shared/services/spinner/spinner.service';
import { ToastService } from '../../shared/services/toast/toast.service';
import { HandlerResponse } from '../models/handlerResponse';

@Injectable()
export class Interceptor implements HttpInterceptor {

  constructor(private readonly spinnerService: SpinnerService, private toastService: ToastService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.spinnerService.show();
    var handlerResponse = new HandlerResponse();
    let statusCode: number;
    let message: string;
    return next.handle(request).pipe(
      tap({
        next: (event) => {
          if (event instanceof HttpResponse) {
            statusCode = handlerResponse.getStatusCode(event.body);
            message = handlerResponse.getMessage(event.body);
            if (event.status === 200) {
              if (statusCode === 201) {
                this.successToast(message);
              }
              else if (statusCode === 202) {
                this.successToast(message);
              }
            }
          }
          return event;
        },
        error: (error) => {
          let statusError: number = error.status;
          statusCode = handlerResponse.getStatusCode(error.error);
          message = handlerResponse.getMessage(error.error);
          if (statusError === 400) {
            if (statusCode === 400) {
              this.errorToast(message);
            }
            else if (statusCode === 404) {
              this.errorToast(message);
            }
            else if (statusCode === 409) {
              this.errorToast(message);
            }
            else if (statusCode === 428) {
              this.errorToast(message);
            }
          }
          else if (statusError === 300) {
            if (statusCode === 304) {
              this.errorToast(message);
            }
          }
          else if (statusError === 500) {
            // ERROR PAGE ABRAHAM
          }
        }
      })
    ).pipe(finalize(() => this.spinnerService.hide()));
  }

  errorToast(message: string) {
    this.toastService.show('Error: ' + message, { classname: 'bg-danger text-light', delay: 15000 });
  }

  successToast(message: string) {
    this.toastService.show(message, { classname: 'bg-success text-light', delay: 10000 });
  }
}
