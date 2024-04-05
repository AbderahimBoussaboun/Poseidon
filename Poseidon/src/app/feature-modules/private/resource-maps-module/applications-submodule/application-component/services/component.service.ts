import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HandlerResponse } from 'src/app/core/models/handlerResponse';
import { ComponentModel } from '../models/component';

@Injectable({
  providedIn: 'root'
})
export class ComponentService {

  private url = 'http://cpdcrprems02.idc.local/PoseidonApi/api/ResourceMaps/Applications';

  constructor(private http: HttpClient) { }

  getAllComponents(idApplication: string, idSubApplication: string): Observable<HandlerResponse> {
    return this.http.get<HandlerResponse>(this.url + '/' + idApplication + '/SubApplications/' + idSubApplication + '/Components');
  }

  addComponent(idApplication: string, idSubApplication: string, component: ComponentModel): Observable<HandlerResponse> {
    return this.http.post<HandlerResponse>(this.url + '/' + idApplication + '/SubApplications/' + idSubApplication + '/Components', component);
  }

  deleteComponent(idApplication: string, idSubApplication: string, idComponent: string): Observable<HandlerResponse> {
    return this.http.delete<HandlerResponse>(this.url + '/' + idApplication + '/SubApplications/' + idSubApplication + '/Components/' + idComponent);
  }

  updateComponent(idApplication: string, idSubApplication: string, idComponent: string, component: ComponentModel): Observable<HandlerResponse> {
    return this.http.put<HandlerResponse>(this.url + '/' + idApplication + '/SubApplications/' + idSubApplication + '/Components/' + idComponent, component);
  }
}
