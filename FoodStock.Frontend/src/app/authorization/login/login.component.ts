import { Component } from '@angular/core';
import { FormBuilder, FormGroup} from '@angular/forms';
import { Router } from '@angular/router';
import { Credentials } from 'src/app/models/credentials';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  loginForm:FormGroup;
  
  constructor(private auth: AuthService, private fb: FormBuilder) {
    this.loginForm = this.fb.group({
      email: [''],
      password: ['']
    })
  }

  login() {
    this.auth.login(this.loginForm.value);
    this.auth.state.subscribe(isAuthenticated => {
      console.log('Zmiana stanu autentykacji:', isAuthenticated);
    });
  }

}

