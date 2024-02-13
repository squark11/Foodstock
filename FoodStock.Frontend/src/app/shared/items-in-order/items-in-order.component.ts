import { Component, Input } from '@angular/core';
import { OrderItem } from 'src/app/models/orderItem';
import { AuthService } from 'src/app/services/auth.service';
import { HttpService } from 'src/app/services/http.service';
import { OrderItemService } from 'src/app/services/order-item.service';

@Component({
  selector: 'app-items-in-order',
  templateUrl: './items-in-order.component.html',
  styleUrls: ['./items-in-order.component.css']
})
export class ItemsInOrderComponent {
  @Input() orderItemId:string;
  orderItems:OrderItem[];
  showModal=false;
  selectedIndex: number;

  ngOnInit(): void {
    this.updateData();
    
  }
  constructor (protected auth:AuthService, private orderItem:OrderItemService){}
 
  showActions = false;

  deleteProduct(id:string) {
    this.orderItem.deleteOrderItem(id).subscribe(
      () => {
        console.log('Order item deleted successfully.');
        this.updateData();
      },
      error => {
        console.error('Error deleting order item:', error);
      }
    );
    console.log(`Usuń produkt `);
  }

  updateData() {
    this.orderItem.getOrder().subscribe(orders => {
      this.orderItems = orders.filter(order => order.orderId === this.orderItemId);
    });
  }

  toggleModal(index: number) {
    this.updateData();
    this.showModal = !this.showModal;
    this.selectedIndex = index;
  }
}
