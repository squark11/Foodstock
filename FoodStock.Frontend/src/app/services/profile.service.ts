import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Observable, filter, map, tap } from 'rxjs';
import { User } from '../models/user';
import { Password } from '../models/password';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  
  private user_request: Observable<User>;
  private url = 'http://localhost/api/accounts';

  constructor(private http: HttpClient, private auth: AuthService) { }

  getUserProfile() {
    if (!this.user_request) {
      this.user_request = this.auth.state.pipe(
        filter(() => this.auth.isAuthenticated),
        map(() => this.auth.getCurrentUser()),
      );
    }
    return this.user_request;
  }

  registerUser(user: User){
    return this.http.post<User>(this.url+"/registerUser", user,{
      headers:{
        'Authorization':'Bearer ' + this.auth.getToken()
      }
    });
  }

  patchUser(user:User){
    return this.http.patch<User>(this.url+ '/' + user.id, user,{
      headers:{
        'Authorization':'Bearer ' + this.auth.getToken()
      }
    });
  }

  patchPass(pass:Password){
    return this.http.patch<Password>(this.url+'/changePassword/'+pass.id, pass,{
      headers:{
        'Authorization':'Bearer ' + this.auth.getToken()
      }
    })
  }
  
  deleteUser(id: string):Observable<{}>{
    return this.http.delete<{}>(this.url+ '/' + id,{
      headers:{
        'Authorization':'Bearer ' + this.auth.getToken()
      }
    }).pipe(tap(console.log));
  }
}
