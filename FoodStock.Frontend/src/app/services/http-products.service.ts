import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { Product } from '../models/product';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class HttpProductsService {
  private url = 'http://localhost/api/products';
  constructor(private http:HttpClient, private auth:AuthService) { }

  postProduct(product: Product){
    return this.http.post<Product>(this.url, product,{
      headers:{
        'Authorization':'Bearer ' + this.auth.getToken()
      }
    });
  }


  
  putProduct(product: Product){
    return this.http.put<Product>(this.url+ '/' + product.id, product,{
      headers:{
        'Authorization':'Bearer ' + this.auth.getToken()
      }
    });
  }

  deleteProduct(id: string):Observable<{}>{
    return this.http.delete<{}>(this.url+ '/' + id,{
      headers:{
        'Authorization':'Bearer ' + this.auth.getToken()
      }
    }).pipe(tap(console.log));
  }
}
