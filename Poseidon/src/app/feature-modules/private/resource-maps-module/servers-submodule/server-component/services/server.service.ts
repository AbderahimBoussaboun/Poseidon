import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HandlerResponse } from 'src/app/core/models/handlerResponse';
import { Server } from '../models/server';

@Injectable({
  providedIn: 'root'
})
export class ServerService {

  private url = 'http://cpdcrprems02.idc.local/PoseidonApi/api/ResourceMaps/Servers';

  constructor(private http: HttpClient) { }

  getAllServers(): Observable<HandlerResponse> {
    return this.http.get<HandlerResponse>(this.url);
  }

  getServerById(id: string): Observable<HandlerResponse> {
    return this.http.get<HandlerResponse>(this.url + '/' + id);
  }

  addServer(server: Server): Observable<HandlerResponse> {
    return this.http.post<HandlerResponse>(this.url, server);
  }

  deleteServer(id: string): Observable<HandlerResponse>{
    return this.http.delete<HandlerResponse>(this.url + '/' + id);
  }

  updateServer(id: string, server: Server): Observable<HandlerResponse>{
    return this.http.put<HandlerResponse>(this.url + '/' + id, server);
  }
}
