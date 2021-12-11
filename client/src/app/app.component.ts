import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IPagination } from './models/pagination';
import { IProduct } from './models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Skinet';
  productsGeneric : IProduct[];

  constructor(private http: HttpClient){

  }

  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/ProductsGeneric')
      .subscribe((response: any) => {
        this.productsGeneric = response.data;
        console.log(this.productsGeneric)
      }, error => {
        console.log(error);
      });
  }
}
