import { Component, Input } from '@angular/core';
import { newOrder } from 'src/app/models/newOrder';
import { Order } from 'src/app/models/order';
import { Supplier } from 'src/app/models/supplier';
import { HttpService } from 'src/app/services/http.service';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent {
  subcategoriesVisible = false;
  showActions = false;
  addInputActive = false;
  suppliers:Supplier[];
  selectedOrder:number;

  selectedSupplierId:newOrder = {
    supplierId: ''
  };

  orders:Order[];

  constructor(private http:HttpService, private order:OrderService){}
  ngOnInit(): void {
    this.getOrders();
    this.http.getSuppliers().subscribe(suppliers=>this.suppliers=suppliers)
  }

  getOrders() {
    this.order.getOrders().subscribe(orders => {
      this.orders = orders.sort((a, b) => {
        const statusOrderA = a.orderStatus.toLowerCase();
        const statusOrderB = b.orderStatus.toLowerCase();
  
        if (statusOrderA === 'new') {
          return -1; 
        } else if (statusOrderB === 'new') {
          return 1;
        } else if (statusOrderA === 'accepted') {
          return -1; 
        } else if (statusOrderB === 'accepted') {
          return 1;
        } else if (statusOrderA === 'send') {
          return -1; 
        } else if (statusOrderB === 'send') {
          return 1;
        } else if (statusOrderA === 'received') {
          return -1; 
        } else if (statusOrderB === 'received') {
          return 1;
        } else if (statusOrderA === 'rejected') {
          return -1; 
        } else if (statusOrderB === 'rejected') {
          return 1;
        } else {
          return 0;
        }
      });
    });
  }

  toggleSubcategories(): void {
    this.subcategoriesVisible = !this.subcategoriesVisible;
  }
  toggleButton(): void {
    this.addInputActive = !this.addInputActive;
  }

  accept(){
    this.order.addOrder(this.selectedSupplierId).subscribe(
      result => {
        console.log("Udało się! "+this.selectedSupplierId)
        this.getOrders();
      },
      error => {
        console.log(this.selectedSupplierId);
        console.error(error)
      }
    );
    this.toggleButton();
  }

  handleOrder(){
    this.getOrders();
  }

  editProduct(): void {
    // Implementacja edycji produktu
    console.log(`Edytuj produkt:`);
  }


}
