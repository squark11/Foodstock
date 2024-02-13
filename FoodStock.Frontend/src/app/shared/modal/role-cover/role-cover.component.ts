import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Role } from 'src/app/models/role';
import { HttpService } from 'src/app/services/http.service';
import { RolesService } from 'src/app/services/roles.service';

@Component({
  selector: 'app-role-cover',
  templateUrl: './role-cover.component.html',
  styleUrls: ['./role-cover.component.css']
})
export class RoleCoverComponent {
  @Input() roleId: string;
  @Output() closeModalEvent: EventEmitter<void> = new EventEmitter<void>();

  role:Role;
  isEditing: boolean = false;
  editedRole: Role;
  editingField: string = ''; // Dodana zmienna do śledzenia edytowanego pola

  constructor(private http: HttpService, private roleService: RolesService) {}

  ngOnInit(): void {
    this.http.getRole(this.roleId).subscribe(role => this.role=role);
  }

  toggleEdit(fieldName: string): void {
    this.isEditing = !this.isEditing;
    this.editedRole = { ...this.role };
    this.editingField = fieldName; // Ustawia aktualne edytowane pole
  }

  saveChanges(): void {
   
      this.roleService.updateRole(this.editedRole).subscribe(
        () => {
          this.role = { ...this.editedRole };
          this.isEditing = false;
          this.closeModalEvent.emit();
          
        },
        error => {
          console.error('Error updating user:', error);
        }
      );
  }

  cancelEdit(): void {
    this.isEditing = false;
    this.editingField = '';
  }

  deleteRole(id: string) {
    if (confirm('Are you sure you want to delete this role?')) {
      this.roleService.deleteRole(id).subscribe(
        () => {
          console.log('Role deleted successfully');
          this.closeModal();
        },
        error => {
          console.error('Error deleting role:', error);
        }
      );
    }
  }

  closeModal(): void {
    this.closeModalEvent.emit();
  }

}
