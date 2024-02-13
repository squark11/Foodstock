import { Component } from '@angular/core';
import { User } from 'src/app/models/user';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent {
  usersList: User[];
  filteredUsers: User[];
  roleFilter: string = '';
  userId: string;
  activeInfo: boolean = false;
  activeAddUser: boolean = false;
  selectedUser: User;

  constructor(private http: HttpService) {}

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers(){
    this.http.getUsers().subscribe(users => {
      this.usersList = users.sort((a, b) => (a.roleId > b.roleId) ? 1 : -1);
      this.filteredUsers = [...this.usersList];
    });
  }

  get uniqueRoles(): string[] {
    return Array.from(new Set(this.usersList.map(user => user.roleName)));
  }

  search(searchValue: string): void {
    if (searchValue || this.roleFilter) {
      this.filteredUsers = this.usersList.filter(user =>
        (user.firstName.toLowerCase().includes(searchValue.toLowerCase()) ||
        user.surname.toLowerCase().includes(searchValue.toLowerCase())) &&
        (this.roleFilter === '' || user.roleName === this.roleFilter)
      );
    } else {
      this.filteredUsers = [...this.usersList];
    }
  }

  toggleInfo(userId: string): void {
    this.userId = userId;
    this.activeInfo = !this.activeInfo;
    this.selectedUser = this.usersList.find(user => user.id === userId);
  }

  handleModalClose(){
    this.selectedUser = null;
    this.activeInfo = false;
    this.getUsers();
  }

  toggleAddUser(): void {
    this.activeAddUser = !this.activeAddUser;
  }
  
  handleModalAddUserClose(){
    this.activeAddUser = false;
    this.getUsers();
  }
}
