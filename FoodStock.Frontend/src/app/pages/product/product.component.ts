import { Component } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { HttpService } from '../../services/http.service';
import { Product } from '../../models/product';
import { AuthService } from 'src/app/services/auth.service';



@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent {
  products: Product[];
  view:Product[]
  searchValue:string;


  constructor( private http:HttpService, protected auth:AuthService) {}

  ngOnInit() {
    this.http.getProducts().subscribe(products => {
      this.products = products
      this.view = products
    });
  }

search(searchValue: Product[]){
  if(searchValue){
    this.view=searchValue;
  }
}



  // search(searchValue: string){
  //   if (searchValue != "") {
  //       this.View = this.products.filter(product =>
  //       product.name.toLowerCase().includes(searchValue.toLowerCase())
  //     );
  //   }else{
  //     this.View= this.products;
  //   } 
  // }
}
