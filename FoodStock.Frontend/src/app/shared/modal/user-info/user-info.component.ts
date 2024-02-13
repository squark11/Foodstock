import { Component } from '@angular/core';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';
import { ProfileService } from 'src/app/services/profile.service';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.css']
})
export class UserInfoComponent {
  user: User
  isChangePasswordModalOpen = false;
  newPassword: string = '';
  confirmPassword: string = '';
  passwordError: string = ''

  constructor(private auth:AuthService, private profile:ProfileService){}
  ngOnInit(): void {
    this.user = this.auth.getCurrentUser();
  }
  openChangePasswordModal() {
    this.isChangePasswordModalOpen = true;
  }

  closeChangePasswordModal() {
    this.isChangePasswordModalOpen = false;
  }

  savePassword() {
    if (this.newPassword.length < 8) {
      this.passwordError = 'Password must be at least 8 characters long.';
    } else if (this.newPassword !== this.confirmPassword) {
      this.passwordError = 'Passwords do not match.';
    } else {
      // Wywołaj metodę patchPass z usługi
      this.profile.patchPass({
        id: this.user.id,
        password: this.newPassword,
        confirmPassword: this.confirmPassword,
      }).subscribe(
        // Obsłuż sukces, np. zamknij modal
        () => {
          this.closeChangePasswordModal();
          this.auth.logout();
        },
        // Obsłuż błąd, np. wyświetl komunikat
        (error) => {
          console.error('Error updating password:', error);
          // Dodaj odpowiednie działania w przypadku błędu
        }
      );
    }
  }
}
