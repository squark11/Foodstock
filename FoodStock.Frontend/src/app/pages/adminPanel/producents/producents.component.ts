import { Component } from '@angular/core';
import { Producent } from 'src/app/models/producent';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-producents',
  templateUrl: './producents.component.html',
  styleUrls: ['./producents.component.css']
})
export class ProducentsComponent {

producentsList: Producent[];
filteredProducent: Producent[];
activeInfo: boolean = false;
selectedProducent: Producent;
isEditMode = false;

constructor (private http:HttpService){}

ngOnInit(): void {
  this.getProducents();
  
}

getProducents(){
  this.http.getProducents().subscribe(producents =>{
    this.producentsList = producents.sort((a, b) => (a.id > b.id) ? 1 : -1);
    this.filteredProducent = [...this.producentsList];
  })
}
  search(searchValue: string): void {
    if (searchValue) {
      this.filteredProducent = this.producentsList.filter(producent =>
        (producent.name.toLowerCase().includes(searchValue.toLowerCase()))
      );
    } else {
      this.filteredProducent = [...this.producentsList];
    }
  }

  toggleInfo(producentId: string): void {
    this.isEditMode = true;
    this.activeInfo = !this.activeInfo;
    this.selectedProducent = this.producentsList.find(producent => producent.id === producentId);
  }

  toggleAddUser(): void {
    this.isEditMode = false;
    this.selectedProducent =  { id:null, name:''};
    this.activeInfo = !this.activeInfo;
    
  }

  handleModalClose(){
    this.getProducents();
    this.activeInfo = false;
  }
}
