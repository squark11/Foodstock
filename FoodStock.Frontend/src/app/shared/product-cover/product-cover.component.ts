import { Component, Input } from '@angular/core';
import { Category } from 'src/app/models/category';
import { Product } from 'src/app/models/product';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-product-cover',
  templateUrl: './product-cover.component.html',
  styleUrls: ['./product-cover.component.css']
})
export class ProductCoverComponent {
  @Input() products: Product[]
  @Input() category:Category;

  sortField: string;
  sortDirection: 'asc' | 'desc' = 'asc';
  constructor(protected auth:AuthService){}


  
  sort(field: string) {
  if (this.sortField === field) {
    this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
  } else {
    this.sortField = field;
    this.sortDirection = 'asc';
  }

  this.products = [...this.products].sort((a, b) => {
    const valueA = this.getFieldValue(a, field);
    const valueB = this.getFieldValue(b, field);

    if (typeof valueA === 'string' && typeof valueB === 'string') {
      const lowerCaseValueA = valueA.toLowerCase();
      const lowerCaseValueB = valueB.toLowerCase();

      if (lowerCaseValueA < lowerCaseValueB) {
        return this.sortDirection === 'asc' ? -1 : 1;
      } else if (lowerCaseValueA > lowerCaseValueB) {
        return this.sortDirection === 'asc' ? 1 : -1;
      } else {
        return 0;
      }
    } else if (typeof valueA === 'number' && typeof valueB === 'number') {
      return this.sortDirection === 'asc' ? valueA - valueB : valueB - valueA;
    } else {
      return 0;
    }
  });
}

getFieldValue(obj: any, field: string) {
  if (!field || !obj) return null;

  const fields = field.split('.');
  let value = obj;

  for (const f of fields) {
    value = value[f];
    if (value === undefined || value === null) break;
  }

  return value;
}
}
