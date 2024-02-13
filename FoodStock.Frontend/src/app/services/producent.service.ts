import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Producent } from '../models/producent';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProducentService {
  private url = 'http://localhost/api/producents';
  constructor(private http: HttpClient, private auth: AuthService) { }

  addProducents(producent: Producent): Observable<Producent> {
    return this.http.post<Producent>(this.url, producent, {
      headers: {
        'Authorization': 'Bearer ' + this.auth.getToken()
      }
    });
  }

  updateProducents(producent: Producent): Observable<Producent> {
    return this.http.put<Producent>(`${this.url}/${producent.id}`, producent, {
      headers: {
        'Authorization': 'Bearer ' + this.auth.getToken()
      }
    });
  }

  deleteProducents(id: string): Observable<{}> { 
    return this.http.delete<{}>(`${this.url}/${id}`, {
      headers: {
        'Authorization': 'Bearer ' + this.auth.getToken()
      }
    });
  }
}
