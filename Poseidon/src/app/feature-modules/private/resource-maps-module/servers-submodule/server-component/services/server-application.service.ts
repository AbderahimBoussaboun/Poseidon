import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HandlerResponse } from 'src/app/core/models/handlerResponse';
import { ServerApplication } from '../models/serverApplication';

@Injectable({
  providedIn: 'root'
})
export class ServerApplicationService {

  private url = 'http://cpdcrprems02.idc.local/PoseidonApi/api/ResourceMaps/Servers';

  constructor(private http: HttpClient) { }

  getAllServerApplications(idServer: string, idRole: string): Observable<HandlerResponse> {
    return this.http.get<HandlerResponse>(this.url + "/" + idServer + "/Roles/" + idRole + "/ServerApplications");
  }

  addServerApplication(idServer: string, idRole: string, serverApplication: ServerApplication): Observable<HandlerResponse> {
    return this.http.post<HandlerResponse>(this.url + "/" + idServer + "/Roles/" + idRole + "/ServerApplications", serverApplication);
  }

  deleteServerApplication(idServer: string, idRole: string, idServerApplication: string): Observable<HandlerResponse> {
    return this.http.delete<HandlerResponse>(this.url + "/" + idServer + "/Roles/" + idRole + "/ServerApplications/" + idServerApplication);
  }

  updateServerApplication(idServer: string, idRole: string, idServerApplication: string, serverApplication: ServerApplication): Observable<HandlerResponse> {
    return this.http.put<HandlerResponse>(this.url + "/" + idServer + "/Roles/" + idRole + "/ServerApplications/" + idServerApplication, serverApplication);
  }

  getServerApplicationById(idServer: string, idRole: string, idServerApplication: string): Observable<HandlerResponse> {
    return this.http.get<HandlerResponse>(this.url + "/" + idServer + "/Roles/" + idRole + "/ServerApplications/" + idServerApplication);
  }
}
