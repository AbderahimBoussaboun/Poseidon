import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HandlerResponse } from 'src/app/core/models/handlerResponse';
import { Environment } from '../models/environment';

@Injectable({
  providedIn: 'root'
})
export class EnvironmentService {

  private url = 'http://cpdcrprems02.idc.local/PoseidonApi/api/ResourceMaps/Environments';

  constructor(private http: HttpClient) { }

  getAllEnvironments(): Observable<HandlerResponse> {
    return this.http.get<HandlerResponse>(this.url);
  }

  getEnvironmentById(id: string): Observable<HandlerResponse> {
    return this.http.get<HandlerResponse>(this.url + '/' + id);
  }

  addEnvironment(environment: Environment): Observable<HandlerResponse> {
    return this.http.post<HandlerResponse>(this.url, environment);
  }

  deleteEnvironment(id: string): Observable<HandlerResponse> {
    return this.http.delete<HandlerResponse>(this.url + '/' + id);
  }

  updateEnvironment(id: string, environment: Environment): Observable<HandlerResponse> {
    return this.http.put<HandlerResponse>(this.url + '/' + id, environment);
  }
}
