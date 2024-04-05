import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HandlerResponse } from 'src/app/core/models/handlerResponse';
import { Application } from '../models/application';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {

  private url = 'http://cpdcrprems02.idc.local/PoseidonApi/api/ResourceMaps/Applications';

  constructor(private http: HttpClient) { }

  getAllApplications(): Observable<HandlerResponse> {
    return this.http.get<HandlerResponse>(this.url);
  }

  addApplication(application: Application): Observable<HandlerResponse> {
    return this.http.post<HandlerResponse>(this.url, application);
  }

  deteleApplication(id: string): Observable<HandlerResponse> {
    return this.http.delete<HandlerResponse>(this.url + "/" + id);
  }

  getApplicationById(id: string): Observable<HandlerResponse> {
    return this.http.get<HandlerResponse>(this.url + "/" + id);
  }

  updateApplication(id: string, application: Application): Observable<HandlerResponse> {
    return this.http.put<HandlerResponse>(this.url + "/" + id, application);
  }
}
