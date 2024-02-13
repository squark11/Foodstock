import { Component } from '@angular/core';
import { User } from './models/user';
import { ProfileService } from './services/profile.service';
import { Observable, interval } from 'rxjs';
import { AuthService } from './services/auth.service';
import { HttpService } from './services/http.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  isMenuHidden: boolean = false;
  menuTransform: number = 300;
  user:User;
  timeLeft: string;

  constructor(private http:HttpService, private profile:ProfileService, protected auth:AuthService){}

  ngOnInit(): void {
    this.profile.getUserProfile().subscribe(user=>this.user=user);
    this.http.getRole(this.user.roleId).subscribe(role=>this.user.roleName=role.name);
    
    interval(1000).subscribe(() => {
      this.updateTimeLeft();
    });
  }

  toggleMenu() {
    this.isMenuHidden = !this.isMenuHidden;
    this.menuTransform = this.isMenuHidden ? 0 : 300;
  }
  private updateTimeLeft(): void {
    const timeLeft = this.auth.getTimeLeft();
    this.timeLeft = this.formatTime(timeLeft);
  }

  private formatTime(timeInMilliseconds: number): string {
    if (timeInMilliseconds !== null) {
      const minutes = Math.floor(timeInMilliseconds / (1000 * 60));
      const seconds = Math.floor((timeInMilliseconds % (1000 * 60)) / 1000);
      return `${minutes} min ${seconds} sec`;
    }
    return '';
  }
  
}