import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Product } from '../models/product';
import { Supplier } from '../models/supplier';
import { Producent } from '../models/producent';
import { User } from '../models/user';
import { Category } from '../models/category';
import { AuthService } from './auth.service';
import { Role } from '../models/role';
import { Organization } from '../models/organization';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  constructor(private httpClient: HttpClient, private auth:AuthService) {}

  getProducents(): Observable<Producent[]> {
    return this.httpClient.get<Producent[]>("http://localhost/api/producents");
  }
  getProducent(id: string): Observable<Producent> {
    return this.httpClient.get<Producent>("http://localhost/api/producents/"+id);
  }
  getProducts(): Observable<Product[]> {
    return this.httpClient.get<Product[]>("http://localhost/api/products");
  }
  getSuppliers(): Observable<Supplier[]> {
    return this.httpClient.get<Supplier[]>("http://localhost/api/suppliers");
  }
  getSupplier(id: string): Observable<Supplier> {
    return this.httpClient.get<Supplier>("http://localhost/api/suppliers/"+id);
  }
  getUsers(): Observable<User[]> {
    return this.httpClient.get<User[]>("http://localhost/api/accounts",{
      headers:{
        'Authorization':'Bearer ' + this.auth.getToken()
      }
    });
  }
  getInActiveUsers():Observable<User[]>{
    return this.httpClient.get<User[]>("http://localhost/api/accounts/inactiveUsers",{
      headers:{
        'Authorization':'Bearer ' + this.auth.getToken()
      }
    });
  }
  getUser(id:string): Observable<User> {
    return this.httpClient.get<User>("http://localhost/api/accounts/"+id,{
      headers:{
        'Authorization':'Bearer ' + this.auth.getToken()
      }
    });
  }
  getProduct(id: string): Observable<Product> {
    return this.httpClient.get<Product>('http://localhost/api/products/' + id);
  }
  getCategories():Observable<Category[]>{
    return this.httpClient.get<Category[]>("http://localhost/api/categories");
  }
  getCategory(categoryId: string):Observable<Category>{
    return this.httpClient.get<Category>('http://localhost/api/categories/'+ categoryId);
  }
  
  getProductsInCategory(id: string): Observable<Product[]> {
    return this.httpClient.get<Product[]>('http://localhost/api/products/byCategoryId?categoryId='+ id);
  }

  getRoles(): Observable<Role[]> {
    return this.httpClient.get<Role[]>("http://localhost/api/roles",{
      headers:{
        'Authorization':'Bearer ' + this.auth.getToken()
      }
    });
  }
  getRole(id:string): Observable<Role> {
    return this.httpClient.get<Role>("http://localhost/api/roles/"+id,{
      headers:{
        'Authorization':'Bearer ' + this.auth.getToken()
      }
    });
  }
  getOrganizations(): Observable<Organization[]> {
    return this.httpClient.get<Organization[]>("http://localhost/api/organizations",{
      headers:{
        'Authorization':'Bearer ' + this.auth.getToken()
      }
    });
  }
  getOrganization(id:string): Observable<Organization> {
    return this.httpClient.get<Organization>("http://localhost/api/organizations/"+id);
  }
}
