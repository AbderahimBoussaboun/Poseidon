import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HandlerResponse } from 'src/app/core/models/handlerResponse';
import { Role } from '../models/role';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  private url = 'http://cpdcrprems02.idc.local/PoseidonApi/api/ResourceMaps/Servers';

  constructor(private http: HttpClient) { }

  getAllRoles(idServer: string): Observable<HandlerResponse> {
    return this.http.get<HandlerResponse>(this.url + "/" + idServer + "/Roles");
  }

  addRole(idServer: string, role: Role): Observable<HandlerResponse> {
    return this.http.post<HandlerResponse>(this.url + "/" + idServer + "/Roles", role);
  }

  deleteRole(idServer: string, idRole: string): Observable<HandlerResponse> {
    return this.http.delete<HandlerResponse>(this.url + "/" + idServer + "/Roles/" + idRole);
  }

  updateRole(idServer: string, idRole: string, role: Role): Observable<HandlerResponse> {
    return this.http.put<HandlerResponse>(this.url + "/" + idServer + "/Roles/" + idRole, role);
  }

  getRoleById(idServer: string, idRole: string): Observable<HandlerResponse> {
    return this.http.get<HandlerResponse>(this.url + "/" + idServer + "/Roles/" + idRole);
  }
}
