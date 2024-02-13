import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Supplier } from 'src/app/models/supplier';
import { SuppliersService } from 'src/app/services/suppliers.service';

@Component({
  selector: 'app-suppliers-cover',
  templateUrl: './suppliers-cover.component.html',
  styleUrls: ['./suppliers-cover.component.css']
})
export class SuppliersCoverComponent {

  @Input() selectedSupplier: Supplier;
  @Input() isEditMode = false;
  @Output() closeModalEvent: EventEmitter<void> = new EventEmitter<void>();

  constructor(private supplierService: SuppliersService) {}

  onSubmit() {
    if (this.isEditMode) {
      this.updateSupplier();
    } else {
      this.addSupplier();
    }
  }

  addSupplier() {
    const { id, ...newSupplier } = this.selectedSupplier;

    this.supplierService.addSupplier(newSupplier)
      .subscribe(
        (result) => {
          this.closeModal();
        },
        (error) => {
          console.log(newSupplier);
        }
      );
  }

  updateSupplier() {
    this.supplierService.updateSupplier(this.selectedSupplier)
      .subscribe(
        (result) => {
          this.closeModal();
        },
        (error) => {
        }
      );
  }

  deleteSupplier() {
    this.supplierService.deleteSupplier(this.selectedSupplier.id)
      .subscribe(
        (result) => {
          this.closeModal();
        },
        (error) => {
        }
      );
  }

  closeModal(): void {
    this.closeModalEvent.emit();
  }
}
