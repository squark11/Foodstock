import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Order } from 'src/app/models/order';
import { AuthService } from 'src/app/services/auth.service';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-order-cover',
  templateUrl: './order-cover.component.html',
  styleUrls: ['./order-cover.component.css']
})
export class OrderCoverComponent {
  @Input() order:Order;
  @Output() orderDeleted: EventEmitter<string> = new EventEmitter<string>();

  statusActive=false;
  subcategoriesVisible = false;
  addInputActive = false;
  sortDirection: 'asc' | 'desc' = 'asc';
  currentUser:string;
  
  constructor(private orderService:OrderService, private router: Router, protected auth:AuthService){}

  ngOnInit(): void {
    this.currentUser = this.auth.getCurrentUser().roleId;
    
  }

  toggleSubcategories(): void {
    this.subcategoriesVisible = !this.subcategoriesVisible;
    this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
  }

  toggleButton(): void {
    this.addInputActive = !this.addInputActive;
  }

  

  deleteOrder(id: string){
    this.orderService.deleteOrder(id).subscribe(
      () => {
        console.log(`Order with id ${id} deleted successfully.`);
        this.orderDeleted.emit(id);
      },
      error => {
        console.error(`Error deleting order with id ${id}: ${error}`);
      }
    );
  }
  statusModal(){
    this.statusActive = !this.statusActive;
    if(this.statusActive===false){
      this.orderDeleted.emit();
    }
  }
  navigateToAddItem() {
    this.router.navigate(['/add-item'], { queryParams: { orderId: this.order.id } });
  }
}
