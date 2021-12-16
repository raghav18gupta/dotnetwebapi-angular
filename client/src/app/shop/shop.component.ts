import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { IProductBrand } from '../shared/models/productBrand';
import { IProductType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  products: IProduct[];
  productTypes: IProductType[];
  productBrands: IProductBrand[];
  totalCount: number = 18;
  @ViewChild('search', {static:true}) searchTerm: ElementRef;

  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: High to Low', value: 'priceDesc'}
  ];
  

  shopParams = new ShopParams();

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getProductBrands();
    this.getProductTypes();
  }

  getProducts(){
    this.shopService
      .getProducts(this.shopParams)
      .subscribe(response => {
      this.products = response!.data;
      this.shopParams.pageNumber = response!.pageIndex;
      this.shopParams.pageSize = response!.pageSize;
      // this.totalCount = response!.count;
    }, error => {
      console.error(error);
    });
  }

  getProductBrands(){
    this.shopService.getProductBrands().subscribe(response => {
      this.productBrands = [{id: 0, name: "All"}, ...response];
    }, error => {
      console.error(error);
    })
  }

  getProductTypes(){
    this.shopService.getProductTypes().subscribe(response => {
      this.productTypes = [{id: 0, name: "All"}, ...response];
    }, error => {
      console.error(error);
    })
  }

  onBrandSelected(brandId: number){
    this.shopParams.brandId = brandId;
    this.getProducts();
  }

  onTypeSelected(typeId: number){
    this.shopParams.typeId = typeId;
    this.getProducts();
  }

  onSortSelected(sort: string){
    this.shopParams.sort = this.sortOptions.find(x => x.name==sort)!.value;
    this.getProducts();
  }

  onPageChanged(event: any){
    if (this.shopParams.pageNumber !== event) {
      this.shopParams.pageNumber = event;
      this.getProducts();
    }
  }

  onSearch(){
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.getProducts();
  }
  onReset(){
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
