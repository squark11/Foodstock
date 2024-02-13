import { Component } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Observable, switchMap } from 'rxjs';
import { Product } from 'src/app/models/product';
import { HttpService } from 'src/app/services/http.service';
import { Location } from '@angular/common';
import { HttpProductsService } from 'src/app/services/http-products.service';
import { Category } from 'src/app/models/category';
import { Supplier } from 'src/app/models/supplier';
import { Producent } from 'src/app/models/producent';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent {
  product:Product;
  category:Category;
  suppliers: Supplier[]=[];
  producents: Producent[]=[];
  orderModal=false;
  addedStatus=false;
  countdownValue: number;

  constructor(
    private http: HttpService, 
    private route: ActivatedRoute,
    private location:Location,
    private httpProductsServices:HttpProductsService
    ) {
  }
  
  ngOnInit() {
    this.route.paramMap.pipe(
      switchMap((params: ParamMap) => this.http.getProduct(params.get('id')))
    ).subscribe(product => {
      this.product = product;

      this.http.getCategory(this.product.categoryId)
        .subscribe(category => this.product.categoryName = category.categoryName);

      this.http.getSupplier(this.product.supplierId)
        .subscribe(supplier => this.product.supplierName = supplier.name);

      this.http.getProducent(this.product.producentId)
        .subscribe(producent => this.product.producentName = producent.name);

      this.http.getUser(this.product.userId)
        .subscribe(user => this.product.user = user);
    });
  }

  delete(id:string){
    this.httpProductsServices.deleteProduct(id).subscribe(
      result => {
        console.log(result);
        this.goBack();
      },
      error => console.error(error)
    );
  }

  changeStatus(i: boolean): void {
    this.orderModal = !this.orderModal;
    if (i) {
      this.addedStatus = true;
      this.countdownValue = 15;

      const countdownInterval = setInterval(() => {
        this.countdownValue--;

        if (this.countdownValue <= 0) {
          clearInterval(countdownInterval);
          this.addedStatus = false;
        }
      }, 1000);
    }
  }

  goBack() {
    this.location.back();
  }
  
}
