import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Producent } from 'src/app/models/producent';
import { ProducentService } from 'src/app/services/producent.service';

@Component({
  selector: 'app-producents-cover',
  templateUrl: './producents-cover.component.html',
  styleUrls: ['./producents-cover.component.css']
})
export class ProducentsCoverComponent {

  @Input() selectedProducent: Producent;
  @Input() isEditMode = false;
  @Output() closeModalEvent: EventEmitter<void> = new EventEmitter<void>();

  constructor(private producent:ProducentService){}

 

  onSubmit() {
    if (this.isEditMode) {
      this.updateProducer();
    } else {
      this.addProducer();
    }
  }

  addProducer() {
    const { id, ...newProducer } = this.selectedProducent;
    this.producent.addProducents(newProducer)
      .subscribe(
        (result) => {
          this.closeModal();
        },
        (error) => {
          console.log(this.selectedProducent)
        }
      );
  }

  updateProducer() {
    this.producent.updateProducents(this.selectedProducent)
      .subscribe(
        (result) => {
          this.closeModal();
        },
        (error) => {
        }
      );
  }

  deleteProducer() {
    this.producent.deleteProducents(this.selectedProducent.id)
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


