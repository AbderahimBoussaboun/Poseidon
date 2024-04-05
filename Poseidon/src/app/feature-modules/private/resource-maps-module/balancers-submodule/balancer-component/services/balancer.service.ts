import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HandlerResponse } from 'src/app/core/models/handlerResponse';
import { Balancer } from '../models/balancer';

@Injectable({
  providedIn: 'root'
})
export class BalancerService {

  private url = 'http://cpdcrprems02.idc.local/PoseidonApi/api/ResourceMaps/Balancers';

  constructor(private http: HttpClient) { }

  getAllBalancers(): Observable<HandlerResponse> {
    return this.http.get<HandlerResponse>(this.url);
  }

  addBalancer(balancer: Balancer): Observable<HandlerResponse> {
    return this.http.post<HandlerResponse>(this.url, balancer);
  }

  deleteBalancer(id: string): Observable<HandlerResponse> {
    return this.http.delete<HandlerResponse>(this.url + "/" + id);
  }

  updateBalancer(id: string, balancer: Balancer): Observable<HandlerResponse> {
    return this.http.put<HandlerResponse>(this.url + "/" + id, balancer);
  }
}
