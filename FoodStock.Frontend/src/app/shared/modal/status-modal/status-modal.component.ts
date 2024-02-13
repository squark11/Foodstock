import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Order } from 'src/app/models/order';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-status-modal',
  templateUrl: './status-modal.component.html',
  styleUrls: ['./status-modal.component.css']
})
export class StatusModalComponent {
  @Input() order: Order;
  @Output() closeModalEvent: EventEmitter<void> = new EventEmitter<void>();

  constructor(private orderService:OrderService){}
  selectedStatus = {
    orderStatus: 'New'
  }
  ngOnInit(): void {
    this.selectedStatus.orderStatus=this.order.orderStatus;
    
  }
  onStatusChange(): void {
    console.log(this.order);
    this.orderService.updateOrderStatus(this.order.id, this.selectedStatus).subscribe(
      (updatedOrder) => {
        console.log('Order status updated successfully:', updatedOrder);
        this.closeModal();
      },
      (error) => {
        console.error('Error updating order status:', error);
      }
    );
  }

  closeModal(): void {
    this.closeModalEvent.emit();
  }
}
