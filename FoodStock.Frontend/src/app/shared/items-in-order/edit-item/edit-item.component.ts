import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Category } from 'src/app/models/category';
import { OrderItem } from 'src/app/models/orderItem';
import { Producent } from 'src/app/models/producent';
import { HttpService } from 'src/app/services/http.service';
import { OrderItemService } from 'src/app/services/order-item.service';

@Component({
  selector: 'app-edit-item',
  templateUrl: './edit-item.component.html',
  styleUrls: ['./edit-item.component.css']
})
export class EditItemComponent {
  @Input() orderItem:OrderItem;
  @Output() closeModalEvent = new EventEmitter<OrderItem>();

  categories:Category[];
  producents:Producent[];
  
  showModal: boolean = false;

  constructor(private orderItemService: OrderItemService, private http:HttpService) {}

  ngOnInit() {
    console.log(this.orderItem);
    this.http.getCategories().subscribe(categories=> this.categories=categories);
    this.http.getProducents().subscribe(producents=>this.producents=producents)
  }

  submitForm() {
    console.log(this.orderItem);
      this.orderItemService.patchOrderItem(this.orderItem.id, this.orderItem).subscribe(
        result => {
          this.closeModal()
        },
        error => {
        }
      )
  }


  closeModal() {
    this.closeModalEvent.emit();
  }
}
