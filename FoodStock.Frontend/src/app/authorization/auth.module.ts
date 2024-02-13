import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../services/auth.service';
import { LoginComponent } from './login/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { authorizedGuard } from './authorized.guard';




@NgModule({
  declarations: [LoginComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  providers: [AuthService, authorizedGuard],
  exports:[LoginComponent]
})
export class AuthModule { }
