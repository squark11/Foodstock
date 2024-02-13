import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Category } from 'src/app/models/category';
import { HttpCategoryService } from 'src/app/services/http-category.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent {
  constructor(private httpCategory:HttpCategoryService, private router: Router, private route: ActivatedRoute,){}
  

  @Input() category: Category;
  @Output() editStatusChanged = new EventEmitter<boolean>();
  categoryName:string;
  ngOnInit(): void {
    this.categoryName=this.category.categoryName;
    
  }

  edit(){
    this.category.categoryName=this.categoryName;
    this.httpCategory.putCategory(this.category).subscribe(
      result => {
        const categoryId = this.route.snapshot.paramMap.get('id');
        this.router.navigate(['/category', categoryId]);
        this.editStatusChanged.emit(false)

      },
      error => console.error(error)
    )
  }
  changeStatus(){
    this.editStatusChanged.emit(false);
  }
}
