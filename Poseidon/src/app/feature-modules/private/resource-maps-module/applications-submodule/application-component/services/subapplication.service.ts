import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HandlerResponse } from 'src/app/core/models/handlerResponse';
import { SubApplication } from '../models/subapplications';

@Injectable({
  providedIn: 'root'
})
export class SubapplicationService {

  private url = 'http://cpdcrprems02.idc.local/PoseidonApi/api/ResourceMaps/Applications';

  constructor(private http: HttpClient) { }

  getAllSubApplications(idApplication: string): Observable<HandlerResponse> {
    return this.http.get<HandlerResponse>(this.url + "/" + idApplication + "/SubApplications");
  }

  addSubApplication(idApplication: string, subapplication: SubApplication): Observable<HandlerResponse> {
    return this.http.post<HandlerResponse>(this.url + "/" + idApplication + "/SubApplications", subapplication);
  }

  deleteSubApplication(idApplication: string, idSubApplication: string): Observable<HandlerResponse> {
    return this.http.delete<HandlerResponse>(this.url + "/" + idApplication + "/SubApplications/" + idSubApplication);
  }

  updateSubApplication(idApplication: string, idSubApplication: string, subapplication: SubApplication): Observable<HandlerResponse> {
    return this.http.put<HandlerResponse>(this.url + "/" + idApplication + "/SubApplications/" + idSubApplication, subapplication);
  }

  getSubApplicationById(idApplication: string, idSubApplication: string): Observable<HandlerResponse> {
    return this.http.get<HandlerResponse>(this.url + "/" + idApplication + "/SubApplications/" + idSubApplication);
  }
}
