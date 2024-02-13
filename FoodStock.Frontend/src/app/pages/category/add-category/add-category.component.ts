import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Category } from 'src/app/models/category';
import { HttpCategoryService } from 'src/app/services/http-category.service';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent {
  @Output() closePopUp = new EventEmitter<boolean>();

  model:Partial<Category> = {};

  constructor(private httpCategoryServices:HttpCategoryService, private route:Router){}
  @Input() categories:Category[];

  closeComponent(){
    this.closePopUp.emit();
  }
  send() {
    if (this.model.categoryName && !this.categories.find(category => category.categoryName === this.model.categoryName)) {
      this.httpCategoryServices.postCategory(this.model).subscribe(
        result => this.closePopUp.emit(),
        error => console.error(error)
      );
    }
  }
}
