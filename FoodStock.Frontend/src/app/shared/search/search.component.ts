import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Product } from 'src/app/models/product';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {
  @Output() searchValueChanged = new EventEmitter<Product[]>();

  View: Product[];
  searchValue: string;

  @Input() products: Product[];


  onSearchValueChange() {
    if (this.searchValue !== '') {
      this.View = this.products.filter(product =>
        product.name.toLowerCase().includes(this.searchValue.toLowerCase())
      );
    } else {
      this.View = this.products;
    }
    this.searchValueChanged.emit(this.View);
  }
}