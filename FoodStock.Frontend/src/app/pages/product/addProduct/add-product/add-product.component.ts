import {Component} from '@angular/core';
import { Route, Router, RouterLink } from '@angular/router';
import { Observable } from 'rxjs';
import { Category } from 'src/app/models/category';
import { Producent } from 'src/app/models/producent';
import { Product } from 'src/app/models/product';
import { Supplier } from 'src/app/models/supplier';
import {HttpProductsService} from 'src/app/services/http-products.service';
import {HttpService} from 'src/app/services/http.service';
import { Location } from '@angular/common';
import { ProfileService } from 'src/app/services/profile.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent {
  model:Partial<Product> = {};
  categoryName:string;
  producents:Producent[] = [];
  products:Product[] = [];
  suppliers:Supplier[] = [];
  categories: Category[]=[];

  constructor(
    private location:Location,
    private http: HttpService,
    private httpProductsServices: HttpProductsService,
    private profile:ProfileService,
    private route:Router) {};

  ngOnInit(): void {
    this.http.getProducents().subscribe(producents => (this.producents = producents));
    this.http.getProducts().subscribe(products => (this.products = products));
    this.http.getSuppliers().subscribe(suppliers => (this.suppliers = suppliers));
    this.http.getCategories().subscribe(categories =>(this.categories=categories));
  }

  isExpirationDateLessThanOneWeek(): boolean {
    const currentDate = new Date();
    const expirationDate = new Date(this.model.expirationDate);
    const deliveryDate = new Date(this.model.deliveryDate)
    const oneWeekFromToday = new Date(currentDate.getTime() + 7 * 24 * 60 * 60 * 1000);
    return expirationDate <= oneWeekFromToday && expirationDate < deliveryDate;
  }

  send() {
    this.profile.getUserProfile().subscribe(user=>this.model.userId=user.id);

    if (this.isExpirationDateLessThanOneWeek()) {
      return;
    }
    
    this.httpProductsServices.postProduct(this.model as Product).subscribe(
      result => this.route.navigate(['/products']),
      error => console.error(error)
    )

  }
  goBack() {
    this.location.back();
  }
}

