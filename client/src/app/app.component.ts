import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { observable } from 'rxjs';
import { IPagination } from './Models/pagination';
import { IProduct } from './Models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Skinet';
  products!:IProduct[];
  
  constructor(private _http: HttpClient) {}

  ngOnInit(): void {
    this._http.get<IPagination>('https://localhost:7246/api/products?pageSize=50').subscribe((response: IPagination) => {
      this.products = response.data;
    }, error => {
      console.log(error);
    })
  }
}
