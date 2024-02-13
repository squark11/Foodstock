import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Role } from 'src/app/models/role';
import { User } from 'src/app/models/user';
import { HttpService } from 'src/app/services/http.service';
import { ProfileService } from 'src/app/services/profile.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent {
  @Input() userId: string;
  @Output() closeModalEvent: EventEmitter<void> = new EventEmitter<void>();

  user: User;
  isEditing: boolean = false;
  editedUser: User;
  uniqueRoles: Role[]=[];
  confirmedPassword: string = '';
  passwordsMatch: boolean = true;
  editingField: string = ''; // Dodana zmienna do śledzenia edytowanego pola

  constructor(private http: HttpService, private userService: ProfileService) {}

  ngOnInit(): void {
    this.http.getRoles().subscribe(roles => this.uniqueRoles = roles);

    this.http.getUser(this.userId).subscribe(user => {
      this.user = user;
      this.editedUser = { ...user };
    });
  }

  toggleEdit(fieldName: string): void {
    this.isEditing = !this.isEditing;
    this.editedUser = { ...this.user };
    this.passwordsMatch = true;
    this.confirmedPassword = '';
    this.editingField = fieldName; // Ustawia aktualne edytowane pole
  }

  checkPasswordMatch(): void {
    this.passwordsMatch = this.editedUser.password === this.confirmedPassword;
  }

  saveChanges(): void {
    if (this.passwordsMatch) {
      this.userService.patchUser(this.editedUser).subscribe(
        () => {
          this.user = { ...this.editedUser };
          this.isEditing = false;
          this.closeModalEvent.emit();
        },
        error => {
          console.error('Error updating user:', error);
        }
      );
    }
  }

  cancelEdit(): void {
    this.isEditing = false;
    this.passwordsMatch = true;
    this.confirmedPassword = '';
    this.editingField = '';
  }

  deleteUser(): void {
    this.userService.deleteUser(this.userId).subscribe(
      () => {
        console.log('User deleted successfully!');
        this.closeModal();
      },
      (error) => {
        console.error('Error deleting user:', error);
      }
    );
  }

  closeModal(): void {
    this.closeModalEvent.emit();
  }
}
