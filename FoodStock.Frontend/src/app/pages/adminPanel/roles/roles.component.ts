import { Component } from '@angular/core';
import { Role } from 'src/app/models/role';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css']
})
export class RolesComponent {
  rolesList: Role[];
  filteredRoles: Role[];
  roleFilter: string = '';
  roleId: string;
  activeInfo: boolean = false;
  activeAddRole: boolean = false;
  selectedUser: Role;

  constructor(private http:HttpService){}

  ngOnInit(): void {
    this.getRole();
  }

  getRole(){
  this.http.getRoles().subscribe(users => {
    this.rolesList = users.sort((a, b) => (a.id > b.id) ? -1 : 1);
    this.filteredRoles = [...this.rolesList];
  });
}
  search(searchValue: string): void {
    if (searchValue || this.roleFilter) {
      this.filteredRoles = this.rolesList.filter(role =>
        (role.name.toLowerCase().includes(searchValue.toLowerCase()) &&
        (this.roleFilter === '' || role.name === this.roleFilter)
      ));
    } else {
      this.filteredRoles = [...this.rolesList];
    }
  }

  toggleInfo(roleId: string): void {
    this.roleId = roleId;
    this.activeInfo = !this.activeInfo;
    this.selectedUser = this.rolesList.find(role => role.id === roleId);
  }

  toggleAddRole() {
    this.activeAddRole = !this.activeAddRole;
  }

  handleModalClose(): void {
    this.selectedUser = null;
    this.activeInfo = false;
    this.getRole();
  }

  handleModalAddRoleClose(){
      this.activeAddRole = false;
      this.getRole();
  }
}
