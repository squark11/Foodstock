import { Component } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Observable, switchMap, tap } from 'rxjs';
import { Product } from 'src/app/models/product';
import { HttpService } from 'src/app/services/http.service';
import { Location } from '@angular/common';
import { HttpProductsService } from 'src/app/services/http-products.service';
import { Producent } from 'src/app/models/producent';
import { Supplier } from 'src/app/models/supplier';
import { Category } from 'src/app/models/category';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css']
})
export class EditProductComponent {
  productDetails:Product;
  categories: Category[]=[];
  suppliers: Supplier[]=[];
  producents: Producent[]=[];

  constructor(
    private http: HttpService, 
    private route: ActivatedRoute,
    private location:Location,
    private httpProductsServices: HttpProductsService,
    private auth:AuthService
    ) {
  }
  ngOnInit() {
    this.route.paramMap.pipe(
      switchMap((params: ParamMap)=> this.http.getProduct(params.get('id')))
    ).subscribe(category => this.productDetails = category);

    this.http.getCategories().subscribe(categories =>(this.categories=categories));
    this.http.getSuppliers().subscribe(suppliers =>(this.suppliers=suppliers));
    this.http.getProducents().subscribe(producents =>(this.producents=producents));
  }

  isExpirationDateLessThanOneWeek(): boolean {
    const currentDate = new Date();
    const expirationDate = new Date(this.productDetails.expirationDate);
    const deliveryDate = new Date(this.productDetails.deliveryDate)
    const oneWeekFromToday = new Date(currentDate.getTime() + 7 * 24 * 60 * 60 * 1000);
    return expirationDate <= oneWeekFromToday && expirationDate < deliveryDate;
  }

  goBack() {
    this.location.back();
  }
  edit(){
    this.productDetails.userId=this.auth.getCurrentUser().id;
    this.httpProductsServices.putProduct(this.productDetails).subscribe(
      result => this.goBack(),
      error => console.error(error)
    )
  }
}

  