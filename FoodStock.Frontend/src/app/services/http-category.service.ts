import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Category } from '../models/category';
import { Observable, tap } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class HttpCategoryService {


  private url = 'http://localhost/api/categories';
  constructor(private http:HttpClient, private auth:AuthService) { }

  postCategory(category: Category){
    return this.http.post<Category>(this.url, category,{
      headers:{
        'Authorization':'Bearer ' + this.auth.getToken()
      }
    });
  }

  putCategory(category: Category){
    return this.http.put<Category>(this.url+"/"+ category.id, category,{
      headers:{
        'Authorization':'Bearer ' + this.auth.getToken()
      }
    });
  }

  deleteCategory(id: string):Observable<{}>{
    return this.http.delete<{}>(this.url+"/"+id,{
      headers:{
        'Authorization':'Bearer ' + this.auth.getToken()
      }
    }).pipe(tap(console.log));
  }
}
