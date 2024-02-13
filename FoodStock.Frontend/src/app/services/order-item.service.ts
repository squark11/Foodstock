import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { OrderItem } from '../models/orderItem';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrderItemService {
  private url = 'http://localhost/api/orderitems';
  constructor(private http: HttpClient, private auth: AuthService) { }

  getOrder(): Observable<OrderItem[]> {
    return this.http.get<OrderItem[]>(this.url, {
      headers: {
        'Authorization': 'Bearer ' + this.auth.getToken()
      }
    });
  }
  
  addOrderItem(orderItem: OrderItem): Observable<OrderItem> {
    return this.http.post<OrderItem>(this.url, orderItem, {
      headers: {
        'Authorization': 'Bearer ' + this.auth.getToken()
      }
    });
  }

  addOrderByProduct(productId:string, object):Observable<OrderItem>{
    return this.http.post<OrderItem>(this.url+"/byProduct/"+productId, object, {
      headers: {
        'Authorization': 'Bearer ' + this.auth.getToken()
      }
    });
  }


  patchOrderItem(orderItemId: string, updateData: OrderItem): Observable<OrderItem> {
    return this.http.patch<OrderItem>(`${this.url}/${orderItemId}`, updateData, {
      headers: {
        'Authorization': 'Bearer ' + this.auth.getToken()
      }
    });
  }

  deleteOrderItem(id: string): Observable<{}> {
    return this.http.delete<{}>(`${this.url}/${id}`, {
      headers: {
        'Authorization': 'Bearer ' + this.auth.getToken()
      }
    });
  }
}
