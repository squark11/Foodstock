import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, timer } from 'rxjs';
import { map, tap, switchMap, take } from 'rxjs/operators';
import { Credentials } from '../models/credentials';
import { User } from '../models/user';
import { Router } from '@angular/router';

interface Session {
  token: string;
  user: User;
  expirationTime: number;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private url = 'http://localhost/api/accounts/login';

  private session = new BehaviorSubject<Session>(this.retrieveSessionFromLocalStorage());

  constructor(private http: HttpClient, private router: Router) {
    timer(0, 60000)
      .pipe(
        switchMap(() => this.session.pipe(take(1)))
      )
      .subscribe(session => {
        if (session && session.expirationTime < Date.now()) {
          this.logout();
        }
      });
  }

  isAdmin(): boolean {
    if(this.getCurrentUser().roleId==="b2cd66ff-1c66-4e0a-a145-3fd9f408a99c"){
      return true;
    }else{
      return false;
    }
    
  }
  isAuthenticated = false;

  state = this.session.pipe(
    map(session => !!session),
    tap(state => this.isAuthenticated = state)
  );

  getToken(): string {
    const session = this.session.getValue();
    return session && session.token;
  }

  getCurrentUser(): User {
    const session = this.session.getValue();
    return session && session.user;
  }

  getTimeLeft(): number | null {
    const session = this.session.getValue();
    if (session) {
      const timeLeft = session.expirationTime - Date.now();
      return timeLeft > 0 ? timeLeft : 0;
    }
    return null;
  }
  
  headers = new HttpHeaders({
    'Content-Type': 'application/json',
  });

  login(credential: Credentials) {
    this.http.post(this.url, credential)
      .subscribe(
        (session: Session) => {
          session.expirationTime = Date.now() + 20 * 60 * 1000;
          this.session.next(session);
          this.saveSessionToLocalStorage(session);
        },
        error => {
          if (error instanceof HttpErrorResponse) {
            console.error('Error Response Headers:', error.headers);
            console.error(error.error);
          }
        }
      );
  }

  logout() {
    this.session.next(null);
    localStorage.removeItem('session');
    this.router.navigate(['/products']);
  }

  private saveSessionToLocalStorage(session: Session) {
    localStorage.setItem('session', JSON.stringify(session));
  }

  private retrieveSessionFromLocalStorage(): Session | null {
    const sessionString = localStorage.getItem('session');
    return sessionString ? JSON.parse(sessionString) : null;
  }
}
