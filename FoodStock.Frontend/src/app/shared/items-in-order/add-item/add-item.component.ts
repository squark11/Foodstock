import { Location } from '@angular/common';
import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Category } from 'src/app/models/category';
import { OrderItem } from 'src/app/models/orderItem';
import { Producent } from 'src/app/models/producent';
import { HttpService } from 'src/app/services/http.service';
import { OrderItemService } from 'src/app/services/order-item.service';

@Component({
  selector: 'app-add-item',
  templateUrl: './add-item.component.html',
  styleUrls: ['./add-item.component.css']
})

export class AddItemComponent {
  orderItem: OrderItem = {
    name: '',
    orderId: '',
    quantity: 0,
    categoryId: '',
    producentId: '',
    barCode: ''
  };

  categories:Category[];
  producents:Producent[];

  constructor(private orderItemService: OrderItemService, private location:Location, private route: ActivatedRoute, private http:HttpService) {}

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.orderItem.orderId = params['orderId'];
    });

    this.http.getCategories().subscribe(categories=> this.categories=categories);
    this.http.getProducents().subscribe(producents=>this.producents=producents)
  }

  submitForm() {
    console.log(this.orderItem);
      this.orderItemService.addOrderItem(this.orderItem).subscribe(
        result => {
          this.goBack();
          console.log('Order item added:', result);
        },
        error => {
          console.error('Error adding order item:', error);
        }
      )
  }

  goBack() {
    this.location.back();
  }
}
