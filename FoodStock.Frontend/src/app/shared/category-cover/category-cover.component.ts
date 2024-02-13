import { Component, Input } from '@angular/core';
import { Category } from 'src/app/models/category';

@Component({
  selector: 'app-category-cover',
  templateUrl: './category-cover.component.html',
  styleUrls: ['./category-cover.component.css']
})
export class CategoryCoverComponent {
  @Input() category:Category;

  getImagePath(category): string {
    return `assets/images/${category.toLowerCase()}.png`;
  }
}
