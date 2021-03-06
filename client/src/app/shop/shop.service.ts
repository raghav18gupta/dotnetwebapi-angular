import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { delay, map } from 'rxjs/operators';
import { IPagination } from '../shared/models/pagination';
import { IProduct } from '../shared/models/product';
import { IProductBrand } from '../shared/models/productBrand';
import { IProductType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) {}

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();
    if (shopParams.brandId  !== 0) {
      params = params.append('brandId', shopParams.brandId.toString());
    }
    if (shopParams.typeId !== 0) {
      params = params.append('typeId', shopParams.typeId.toString());
    }
    if(shopParams.search){
      params = params.append('search', shopParams.search);
    }

    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber.toString());
    params = params.append('pageSize', shopParams.pageSize.toString());
    
    return this.http.get<IPagination>(
        this.baseUrl + 'productsGeneric',
        { observe: 'response', params}
      ).pipe(
        map(response =>{
          return response.body;
        })
      );
  }
  
  getProduct(id: number){
    console.clear();
    console.log(id);
    return this.http.get<IProduct>(this.baseUrl + 'productsGeneric/' + id);
  }
  getProductBrands() {
    return this.http.get<IProductBrand[]>(
      this.baseUrl + 'productsGeneric/brands'
    );
  }

  getProductTypes() {
    return this.http.get<IProductType[]>(
      this.baseUrl + 'productsGeneric/types'
    );
  }
}
