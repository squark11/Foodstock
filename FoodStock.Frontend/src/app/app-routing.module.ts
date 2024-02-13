import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductComponent } from './pages/product/product.component';
import { CategoryComponent } from './pages/category/category.component';
import { AddProductComponent } from './pages/product/addProduct/add-product/add-product.component';
import { ProductDetailsComponent } from './pages/product/product-details/product-details.component';
import { EditProductComponent } from './pages/product/edit-product/edit-product.component';
import { ProductsInCategoryComponent } from './pages/category/products-in-category/products-in-category.component';
import { authorizedGuard } from './authorization/authorized.guard';
import { AdminPanelComponent } from './pages/adminPanel/admin-panel.component';
import { UserInfoComponent } from './shared/modal/user-info/user-info.component';
import { OrdersComponent } from './pages/orders/orders.component';
import { AdminAuthGuard } from './authorization/adminAuthGuard';
import { AddItemComponent } from './shared/items-in-order/add-item/add-item.component';

const routes: Routes = [
  { path: '', component: ProductComponent},
  { path: 'products', component: ProductComponent},
  { path: 'products/:id', component: ProductDetailsComponent,canActivate:[authorizedGuard]},
  { path: 'add-product', component: AddProductComponent,canActivate:[authorizedGuard]},
  { path: 'edit-product/:id', component: EditProductComponent, canActivate:[authorizedGuard]},
  { path: 'category', component: CategoryComponent },
  { path: 'category/:id', component: ProductsInCategoryComponent },
  { path: 'admin', component: AdminPanelComponent, canActivate:[AdminAuthGuard]},
  { path: 'profile', component: UserInfoComponent },
  { path: 'orders', component: OrdersComponent, canActivate:[authorizedGuard] },
  { path: 'add-item', component: AddItemComponent, canActivate:[authorizedGuard] },
 

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
