import { Component } from '@angular/core';
import { Category } from 'src/app/models/category';
import { AuthService } from 'src/app/services/auth.service';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent {
  categories: Category[];
  View: Category[];
  inputValue: string;
  addCategory: boolean = false;

  constructor(private http: HttpService, protected auth:AuthService) {}

  ngOnInit(): void {
    this.http.getCategories().subscribe(categories => {
      this.categories = categories;
      this.View = this.categories;
    });
  }

  search(): void {
    if (this.inputValue !== '') {
      this.View = this.categories.filter(category =>
        category.categoryName.toLowerCase().includes(this.inputValue.toLowerCase())
      );
    } else {
      this.View = this.categories;
    }
  }
}
