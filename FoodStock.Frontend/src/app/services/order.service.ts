import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Order } from '../models/order';
import { Observable } from 'rxjs';
import { Supplier } from '../models/supplier';
import { newOrder } from '../models/newOrder';
import { OrderItem } from '../models/orderItem';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private http: HttpClient, private auth: AuthService) { }
  private url = 'http://localhost/api/orders';


  getOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.url, {
      headers: {
        'Authorization': 'Bearer ' + this.auth.getToken()
      }
    });
  }
  
  addOrder(supplierId: newOrder): Observable<Order> {
    return this.http.post<Order>(this.url, supplierId, {
      headers: {
        'Authorization': 'Bearer ' + this.auth.getToken()
      }
    });
  }

  updateOrderStatus(orderId: string, orderStatus): Observable<Order> {
    return this.http.patch<Order>(`${this.url}/${orderId}`, orderStatus, {
      headers: {
        'Authorization': 'Bearer ' + this.auth.getToken()
      }
    });
  }

  deleteOrder(id: string): Observable<{}> {
    return this.http.delete<{}>(`${this.url}/${id}`, {
      headers: {
        'Authorization': 'Bearer ' + this.auth.getToken()
      }
    });
  }

}
