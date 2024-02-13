import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Role } from '../models/role';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RolesService {
  private url = 'http://localhost/api/roles';

  constructor(private http: HttpClient, private auth: AuthService) { }
  
  addRole(role: Role): Observable<Role> {
    return this.http.post<Role>(this.url, role, {
      headers: {
        'Authorization': 'Bearer ' + this.auth.getToken()
      }
    });
  }

  updateRole(role: Role): Observable<Role> {
    return this.http.put<Role>(`${this.url}/${role.id}`, role, {
      headers: {
        'Authorization': 'Bearer ' + this.auth.getToken()
      }
    });
  }

  deleteRole(id: string): Observable<{}> {
    return this.http.delete<{}>(`${this.url}/${id}`, {
      headers: {
        'Authorization': 'Bearer ' + this.auth.getToken()
      }
    });
  }
}
