import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ProductComponent } from './pages/product/product.component';
import { CategoryComponent } from './pages/category/category.component';
import { FormsModule } from '@angular/forms';
import { ProductsInCategoryComponent } from './pages/category/products-in-category/products-in-category.component';
import { ProductDetailsComponent } from './pages/product/product-details/product-details.component';
import { AddProductComponent } from './pages/product/addProduct/add-product/add-product.component';
import { ProductCoverComponent } from './shared/product-cover/product-cover.component';
import { EditProductComponent } from './pages/product/edit-product/edit-product.component';
import { SearchComponent } from './shared/search/search.component';
import { CategoryCoverComponent } from './shared/category-cover/category-cover.component';
import { EditComponent } from './pages/category/products-in-category/edit/edit.component';
import { AddCategoryComponent } from './pages/category/add-category/add-category.component';
import { AuthModule } from './authorization/auth.module';
import { AdminPanelComponent } from './pages/adminPanel/admin-panel.component';
import { CardComponent } from './pages/adminPanel/card/card.component';
import { UsersComponent } from './pages/adminPanel/users/users.component';
import { UserComponent } from './shared/modal/user/user.component';
import { AddUserComponent } from './pages/adminPanel/users/add-user/add-user.component';
import { RolesComponent } from './pages/adminPanel/roles/roles.component';
import { RoleCoverComponent } from './shared/modal/role-cover/role-cover.component';
import { AddRoleComponent } from './pages/adminPanel/roles/add-role/add-role.component';
import { ProducentsComponent } from './pages/adminPanel/producents/producents.component';
import { ProducentsCoverComponent } from './shared/modal/producents-cover/producents-cover.component';
import { SuppliersCoverComponent } from './shared/modal/suppliers-cover/suppliers-cover.component';
import { SuppliersComponent } from './pages/adminPanel/suppliers/suppliers.component';
import { OrganizationsComponent } from './pages/adminPanel/organizations/organizations.component';
import { OrganizationsCoverComponent } from './shared/modal/organizations-cover/organizations-cover.component';
import { UserInfoComponent } from './shared/modal/user-info/user-info.component';
import { OrdersComponent } from './pages/orders/orders.component';
import { OrderCoverComponent } from './shared/order-cover/order-cover.component';
import { ItemsInOrderComponent } from './shared/items-in-order/items-in-order.component';
import { AddItemComponent } from './shared/items-in-order/add-item/add-item.component';
import { EditItemComponent } from './shared/items-in-order/edit-item/edit-item.component';
import { StatusModalComponent } from './shared/modal/status-modal/status-modal.component';
import { AddProductByIdComponent } from './shared/modal/add-product-by-id/add-product-by-id.component';


@NgModule({
  declarations: [
    AppComponent,
    ProductComponent,
    CategoryComponent,
    SuppliersComponent,
    ProductsInCategoryComponent,
    ProductDetailsComponent,
    AddProductComponent,
    ProductCoverComponent,
    EditProductComponent,
    SearchComponent,
    CategoryCoverComponent,
    EditComponent,
    AddCategoryComponent,
    AdminPanelComponent,
    CardComponent,
    UsersComponent,
    UserComponent,
    AddUserComponent,
    RolesComponent,
    RoleCoverComponent,
    AddRoleComponent,
    ProducentsComponent,
    ProducentsCoverComponent,
    SuppliersCoverComponent,
    OrganizationsComponent,
    OrganizationsCoverComponent,
    UserInfoComponent,
    OrdersComponent,
    OrderCoverComponent,
    ItemsInOrderComponent,
    AddItemComponent,
    EditItemComponent,
    StatusModalComponent,
    AddProductByIdComponent,
  ],
  imports: [
    FormsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    CommonModule,
    AuthModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule { }
