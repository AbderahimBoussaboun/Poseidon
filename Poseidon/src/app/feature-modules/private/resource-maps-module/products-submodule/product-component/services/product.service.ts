import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HandlerResponse } from 'src/app/core/models/handlerResponse';
import { Product } from '../models/product';

@Injectable({
	providedIn: 'root'
})
export class ProductService {

	private url = 'http://cpdcrprems02.idc.local/PoseidonApi/api/ResourceMaps/Products';


	constructor(private http: HttpClient) { }

	getAllProducts(): Observable<HandlerResponse> {
		return this.http.get<HandlerResponse>(this.url);
	}

	getProductById(id: string): Observable<HandlerResponse> {
		return this.http.get<HandlerResponse>(this.url + "/" + id);
	}

	addProduct(product: Product): Observable<HandlerResponse> {
		return this.http.post<HandlerResponse>(this.url, product);
	}

	deleteProduct(id: string): Observable<HandlerResponse> {
		return this.http.delete<HandlerResponse>(this.url + "/" + id);
	}

	updateProduct(id: string, product: Product): Observable<HandlerResponse> {
		return this.http.put<HandlerResponse>(this.url + "/" + id, product);
	}
}
