import { Component, EventEmitter, Output } from '@angular/core';
import { Role } from 'src/app/models/role';
import { RolesService } from 'src/app/services/roles.service';

@Component({
  selector: 'app-add-role',
  templateUrl: './add-role.component.html',
  styleUrls: ['./add-role.component.css']
})
export class AddRoleComponent {
  @Output() closeModalEvent: EventEmitter<void> = new EventEmitter<void>();

  newRole:Role = {
    name:''
  };
  

  constructor ( private role: RolesService){}

  addRole() {
    this.role.addRole(this.newRole).subscribe(
      (response) => {
        console.log('Role added successfully:', response);
        this.closeModal();
      },
      (error) => {
        console.error('Error adding role:', error);
      }
    );
  }
  closeModal(): void {
    this.closeModalEvent.emit();
  }
}
