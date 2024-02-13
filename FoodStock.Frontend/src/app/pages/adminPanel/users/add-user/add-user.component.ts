import { Component, EventEmitter, Output } from '@angular/core';
import { Role } from 'src/app/models/role';
import { User } from 'src/app/models/user';
import { HttpService } from 'src/app/services/http.service';
import { ProfileService } from 'src/app/services/profile.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent {
  @Output() closeModalEvent: EventEmitter<void> = new EventEmitter<void>();
  
  newUser:User = {
    email: 'example@email.com',
    password: '',
    confirmPassword: '',
    firstName: 'John',
    surname: 'Doe',
    roleId: '', 
  };

  passwordsMatch: boolean = true;
  roles: Role[] = [];
  error:string=null;

  constructor(private http: HttpService, private profile: ProfileService) {}

  ngOnInit(): void {
    this.http.getRoles().subscribe((roles) => {
      this.roles = roles;
      this.newUser.roleId=roles[0].id
    });


  }

  addUser(): void {
    if (this.validateUser()) {
      console.log(this.newUser);
      this.profile.registerUser(this.newUser).subscribe(
        () => {
          console.log('User added successfully!');
          this.closeModalEvent.emit();
        },
        error => {
          console.error('Error adding user:', error);
          
          this.error=error.error.title;
        }
      );
    }
  }

  validateUser(): boolean {
    this.passwordsMatch = this.newUser.password === this.newUser.confirmPassword;
    return this.passwordsMatch;
  }

  closeModal(): void {
    this.closeModalEvent.emit();
  }
}
