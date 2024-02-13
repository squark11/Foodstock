import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Supplier } from '../models/supplier';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SuppliersService {
  private url = 'http://localhost/api/suppliers';

  constructor(private http: HttpClient, private auth: AuthService) { }

  addSupplier(supplier: Supplier): Observable<Supplier> {
    return this.http.post<Supplier>(this.url, supplier, {
      headers: {
        'Authorization': 'Bearer ' + this.auth.getToken()
      }
    });
  }

  updateSupplier(supplier: Supplier): Observable<Supplier> {
    return this.http.put<Supplier>(`${this.url}/${supplier.id}`, supplier, {
      headers: {
        'Authorization': 'Bearer ' + this.auth.getToken()
      }
    });
  }

  deleteSupplier(id: string): Observable<{}> {
    return this.http.delete<{}>(`${this.url}/${id}`, {
      headers: {
        'Authorization': 'Bearer ' + this.auth.getToken()
      }
    });
  }
}
