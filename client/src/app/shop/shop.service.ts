import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { delay, map, Observable } from 'rxjs';
import { IBrand } from '../shared/Models/brand';
import { IPagination } from '../shared/Models/pagination';
import { IType } from '../shared/Models/productType';
import { ShopParams } from '../shared/Models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:7246/api/';

  constructor(private _http: HttpClient) { }

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();

    if(shopParams.brandId !== 0) {
      params = params.append('brandId', shopParams.brandId.toString());
    }

    if(shopParams.typeId !== 0) {
      params = params.append('typeId', shopParams.typeId.toString());
    }

    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber.toString());
    params = params.append('pageSize', shopParams.pageSize.toString());

    return this._http.get<IPagination>(this.baseUrl + 'products', {observe: 'response', params})
                .pipe(
                  map(response => {
                    return response.body;
                  })
                );
  }

  getBrands() : Observable<IBrand[]> {
    return this._http.get<IBrand[]>(this.baseUrl + 'products/brands');
  }

  getTypes() : Observable<IType[]> {
    return this._http.get<IType[]>(this.baseUrl + 'products/types');
  }
}
