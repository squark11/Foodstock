import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { trigger, style, animate, transition } from '@angular/animations';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css'],
  animations: [
    trigger('countTo', [
      transition(':increment', [
        style({ transform: 'translateY(20px)', opacity: 0 }),
        animate('2000ms', style({ transform: 'translateY(0)', opacity: 1 })),
      ]),
    ]),
  ],
})
export class CardComponent implements OnInit {
  @ViewChild('userCount') userCountElement: ElementRef;
  @ViewChild('productCount') productCountElement: ElementRef;
  @ViewChild('supplierCount') supplierCountElement: ElementRef;

  userCount: number = 0;
  categoryCount: number = 0;
  organizationCount: number = 0;
  producentsCount: number = 0;
  productCount: number = 0;
  rolesCount: number = 0;
  supplierCount: number = 0;

  constructor(private http: HttpService) {}

  ngOnInit(): void {
    this.http.getUsers().subscribe((users) => (this.userCount = users.length));
    this.http.getCategories().subscribe((categories) => (this.categoryCount = categories.length));
    this.http.getOrganizations().subscribe((organizations) => (this.organizationCount = organizations.length));
    this.http.getProducents().subscribe((producents) => (this.producentsCount = producents.length));
    this.http.getProducts().subscribe((products) => (this.productCount = products.length));
    this.http.getRoles().subscribe((roles) => (this.rolesCount = roles.length));
    this.http.getSuppliers().subscribe((suppliers) => (this.supplierCount = suppliers.length));
  }

}
