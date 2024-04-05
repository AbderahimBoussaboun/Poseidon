import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HandlerResponse } from 'src/app/core/models/handlerResponse';
import { ComponentType } from '../models/componentType';

@Injectable({
  providedIn: 'root'
})
export class ComponentTypeService {

  private url = 'http://cpdcrprems02.idc.local/PoseidonApi/api/ResourceMaps/ComponentType';

  constructor(private http: HttpClient) { }

  getAllComponentTypes(): Observable<HandlerResponse> {
    return this.http.get<HandlerResponse>(this.url);
  }

  addComponentType(componentType: ComponentType): Observable<HandlerResponse> {
    return this.http.post<HandlerResponse>(this.url, componentType);
  }

  deleteComponentType(id: string): Observable<HandlerResponse> {
    return this.http.delete<HandlerResponse>(this.url + '/' + id);
  }

  updateComponentType(id: string, componentType: ComponentType): Observable<HandlerResponse> {
    return this.http.put<HandlerResponse>(this.url + '/' + id, componentType);
  }

  getComponentTypeById(id: string): Observable<HandlerResponse> {
    return this.http.get<HandlerResponse>(this.url + '/' + id);
  }
}
