import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryListComponent } from './features/category/category-list/category-list.component';
import { AddCategoryComponent } from './features/category/add-category/add-category.component';
import { EditCategoryComponent } from './features/category/edit-category/edit-category.component';
import { ProductListComponent } from './features/blog-post/product-list/product-list.component';
import { AddProductComponent } from './features/blog-post/add-product/add-product.component';
import { EditProductComponent } from './features/blog-post/edit-product/edit-product.component';
import { HomeComponent } from './features/public/home/home.component';
import { ProductDetailsComponent } from './features/public/product-details/product-details.component';
import { LoginComponent } from './features/auth/login/login.component';
import { authGuard } from './features/auth/guards/auth.guard';
import { RegisterComponent } from './features/auth/register/register/register.component';
import { UsersListComponent } from './features/users/users-list/users-list.component';
import { EditUserComponent } from './features/users/edit-user/edit-user.component';
import { BasketComponent } from './features/blog-post/basket/basket/basket.component';
import { ReceiptComponent } from './features/blog-post/receipt/receipt/receipt.component';
import { OrderDetailComponent } from './features/blog-post/order/order.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'product/:url',
    component: ProductDetailsComponent
  },
  {
    path: 'basket',
    component: BasketComponent
  },
  {
    path: 'receipt',
    component: ReceiptComponent
  },
  {
    path: 'admin/categories',
    component: CategoryListComponent,
    canActivate: [authGuard]
  },
  {
    path: 'admin/categories/add',
    component: AddCategoryComponent,
    canActivate: [authGuard]
  },
  {
    path: 'admin/categories/:id',
    component: EditCategoryComponent,
    canActivate: [authGuard] 
  },
  {
    path: 'admin/products',
    component: ProductListComponent,
    canActivate: [authGuard]
  },
  {
    path: 'admin/products/add',
    component: AddProductComponent,
    canActivate: [authGuard]
  },
  {
    path: 'admin/products/:id',
    component: EditProductComponent,
    canActivate: [authGuard]
  },
  {
    path: 'admin/users',
    component: UsersListComponent,
    canActivate: [authGuard]
  },
  {
    path: 'admin/users/:id',
    component: EditUserComponent,
    canActivate: [authGuard]
  },
  {
    path: 'orders/:id',
    component: OrderDetailComponent
    // canActivate: [authGuard]
  },
  {
    path: 'orders',
    component: OrderDetailComponent
    // canActivate: [authGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
