import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Order } from 'src/app/models/order';
import { OrderItem } from 'src/app/models/orderItem';
import { Product } from 'src/app/models/product';
import { OrderItemService } from 'src/app/services/order-item.service';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-add-product-by-id',
  templateUrl: './add-product-by-id.component.html',
  styleUrls: ['./add-product-by-id.component.css']
})
export class AddProductByIdComponent {

@Input() product:Product
@Output() closeModalEvent: EventEmitter<boolean> = new EventEmitter<boolean>();

orderPorduct = {
  quantity: 0,
  orderId:''
};

supplier:string;
orders:Order[]=[];

constructor(private orderService:OrderService, private http:OrderItemService){}

ngOnInit(): void {
  this.orderService.getOrders().subscribe(orders=>{
    this.orders=orders;
    this.orderPorduct.orderId=orders[0].id;
  })
}
closeModal(success: boolean) {
  this.closeModalEvent.emit(success);
}

onSubmit() {
  this.http.addOrderByProduct(this.product.id, this.orderPorduct).subscribe(
    (response) => {
      console.log('Order added successfully', response);
      this.closeModal(true);
    },
    (error) => {
      console.error('Error adding order', error);
    }
  );
}

}
