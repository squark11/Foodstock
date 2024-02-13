import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Organization } from '../models/organization';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrganizationService {
  private url = 'http://localhost/api/organizations';
  constructor(private http: HttpClient, private auth: AuthService) { }
  
  addOrganization(organization: Organization): Observable<Organization> {
    return this.http.post<Organization>(this.url, organization, {
      headers: {
        'Authorization': 'Bearer ' + this.auth.getToken()
      }
    });
  }

  updateOrganization(organization: Organization): Observable<Organization> {
    return this.http.put<Organization>(`${this.url}/${organization.id}`, organization, {
      headers: {
        'Authorization': 'Bearer ' + this.auth.getToken()
      }
    });
  }
}
