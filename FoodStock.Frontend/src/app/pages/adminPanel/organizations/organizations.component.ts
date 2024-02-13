import { Component } from '@angular/core';
import { Organization } from 'src/app/models/organization';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-organizations',
  templateUrl: './organizations.component.html',
  styleUrls: ['./organizations.component.css']
})
export class OrganizationsComponent {
  organizations:Organization[] = [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "name": "ABC Company",
      "ownerName": "John",
      "ownerSurname": "Doe",
      "nip": "123-456-789",
      "country": "USA",
      "city": "New York",
      "cityCode": "10001",
      "street": "Broadway",
      "streetNumber": "123"
    },
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa7",
      "name": "XYZ Corporation",
      "ownerName": "Jane",
      "ownerSurname": "Smith",
      "nip": "987-654-321",
      "country": "Canada",
      "city": "Toronto",
      "cityCode": "M5A 1A1",
      "street": "Bay Street",
      "streetNumber": "456"
    }
  ];
  filteredOrganizations:Organization[];
  selectedOrganization:Organization;
  activeInfo = false;
  isEditMode= true;
  constructor(private http:HttpService){}

  ngOnInit(): void {
    this.http.getOrganizations().subscribe(organizations => {
      console.log(organizations);
      if (organizations && organizations.length > 0) {
  
        this.organizations = organizations.sort((a, b) => {
          const idA = +a.id;
          const idB = +b.id;
          return idA - idB;
        });
        
      } else {
  
        console.log('No organizations found.');
     
      }
      this.filteredOrganizations = [...this.organizations];
    });
  }

  search(searchValue: string): void {
    if (searchValue) {
      this.filteredOrganizations = this.organizations.filter(producent =>
        (producent.name.toLowerCase().includes(searchValue.toLowerCase()))
      );
    } else {
      this.filteredOrganizations = [...this.organizations];
    }
  }
  toggleInfo(organiaztionId: string): void {
    this.activeInfo = !this.activeInfo;
    this.isEditMode
    this.selectedOrganization = this.organizations.find(organiaztion => organiaztion.id === organiaztionId);
  }

  toggleAddOrganization(): void {
    this.isEditMode = false;
    this.selectedOrganization = {} as Organization;
    this.activeInfo = !this.activeInfo;
    
  }

  handleModalClose(){
    this.activeInfo = false;
  }
}
