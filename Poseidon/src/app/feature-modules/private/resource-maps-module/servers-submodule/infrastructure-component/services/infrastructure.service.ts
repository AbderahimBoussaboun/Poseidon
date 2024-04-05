import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HandlerResponse } from 'src/app/core/models/handlerResponse';
import { Infrastructure } from '../models/infrastructure';

@Injectable({
  providedIn: 'root'
})
export class InfrastructureService {

  private url = 'http://cpdcrprems02.idc.local/PoseidonApi/api/ResourceMaps/Infrastructure';

  constructor(private http: HttpClient) { }

  getAllInfrastructures(): Observable<HandlerResponse> {
    return this.http.get<HandlerResponse>(this.url);
  }

  getInfrastructureById(id: string): Observable<HandlerResponse> {
    return this.http.get<HandlerResponse>(this.url + '/' + id);
  }

  addInfrastructure(infrastructure: Infrastructure): Observable<HandlerResponse> {
    return this.http.post<HandlerResponse>(this.url, infrastructure);
  }

  deleteInfrastructure(id: string): Observable<HandlerResponse> {
    return this.http.delete<HandlerResponse>(this.url + '/' + id);
  }

  updateInfrastructure(id: string, infrastructure: Infrastructure): Observable<HandlerResponse> {
    return this.http.put<HandlerResponse>(this.url + '/' + id, infrastructure);
  }
}
