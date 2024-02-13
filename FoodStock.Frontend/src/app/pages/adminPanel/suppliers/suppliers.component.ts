import { Component } from '@angular/core';
import { Supplier } from 'src/app/models/supplier';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-suppliers',
  templateUrl: './suppliers.component.html',
  styleUrls: ['./suppliers.component.css']
})
export class SuppliersComponent {

  suppliersList: Supplier[];
  filteredSupplier: Supplier[];
  activeInfo: boolean = false;
  selectedSupplier: Supplier;
  isEditMode = false;

  constructor(private http: HttpService) {}

  ngOnInit(): void {
    this.getSuppliers();
  }

  getSuppliers() {
    this.http.getSuppliers().subscribe(suppliers => {
      this.suppliersList = suppliers.sort((a, b) => (a.id > b.id) ? 1 : -1);
      this.filteredSupplier = [...this.suppliersList];
    });
  }

  search(searchValue: string): void {
    if (searchValue) {
      this.filteredSupplier = this.suppliersList.filter(supplier =>
        (supplier.name.toLowerCase().includes(searchValue.toLowerCase()))
      );
    } else {
      this.filteredSupplier = [...this.suppliersList];
    }
  }

  toggleInfo(supplierId: string): void {
    this.isEditMode = true;
    this.activeInfo = !this.activeInfo;
    this.selectedSupplier = this.suppliersList.find(supplier => supplier.id === supplierId);
  }

  toggleAddSupplier(): void {
    this.isEditMode = false;
    this.selectedSupplier = { id: null, name: '' };
    this.activeInfo = !this.activeInfo;
  }

  handleModalClose() {
    this.getSuppliers();
    this.activeInfo = false;
  }
}
