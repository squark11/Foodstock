import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Organization } from 'src/app/models/organization';
import { OrganizationService } from 'src/app/services/organization.service';

@Component({
  selector: 'app-organizations-cover',
  templateUrl: './organizations-cover.component.html',
  styleUrls: ['./organizations-cover.component.css']
})
export class OrganizationsCoverComponent {
  @Input() selectedOrganization:Organization;
  @Input() isEditMode = true;
  @Output() closeModalEvent: EventEmitter<void> = new EventEmitter<void>();

  constructor(private organizationService:OrganizationService){}

  onSubmit() {
    if (this.isEditMode) {
      this.updateOrganization();
    } else {
      this.addOrganization();
    }
  }

  private addOrganization() {
    this.organizationService.addOrganization(this.selectedOrganization).subscribe(
      (result) => {
        console.log('Organization added successfully:', result);
        this.closeModal();
      },
      (error) => {
        console.error('Error adding organization:', error);
      }
    );
  }

  private updateOrganization() {
    this.organizationService.updateOrganization(this.selectedOrganization).subscribe(
      (result) => {
        console.log('Organization updated successfully:', result);
        this.closeModal();
      },
      (error) => {
        console.error('Error updating organization:', error);
      }
    );
  }

  closeModal(): void {
    this.closeModalEvent.emit();
  }
}

