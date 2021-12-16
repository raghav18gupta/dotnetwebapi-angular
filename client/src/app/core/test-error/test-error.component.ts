import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent implements OnInit {
  baseUrl = environment.apiUrl;
  validationErrors: any;
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  get404Error(){
    this.http.get(this.baseUrl + 'ProductsGeneric/42').subscribe(response => {
      console.log(response);
    }, error => {
      console.error(error);
    })
  }
  get500Error(){
    this.http.get(this.baseUrl + 'Buggy/serverError').subscribe(response => {
      console.log(response);
    }, error => {
      console.error(error);
    })
  }
  get400Error(){
    this.http.get(this.baseUrl + 'Buggy/badRequest').subscribe(response => {
      console.log(response);
    }, error => {
      console.error(error);
    })
  }
  get400ValidationError(){
    this.http.get(this.baseUrl + 'ProductsGeneric/fortyTwo').subscribe(response => {
      console.log(response);
    }, error => {
      console.error(error);
      this.validationErrors = error.error.errors;
    })
  }

}
