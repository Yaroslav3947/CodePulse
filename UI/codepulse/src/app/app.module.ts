import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './core/components/navbar/navbar.component';
import { CategoryListComponent } from './features/category/category-list/category-list.component';
import { AddCategoryComponent } from './features/category/add-category/add-category.component';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { EditCategoryComponent } from './features/category/edit-category/edit-category.component';
import { ProductListComponent } from './features/blog-post/product-list/product-list.component';
import { AddProductComponent } from './features/blog-post/add-product/add-product.component';
import { MarkdownModule } from 'ngx-markdown';
import { EditProductComponent } from './features/blog-post/edit-product/edit-product.component';
import { ImageSelectorComponent } from './shared/components/image-selector/image-selector.component';
import { HomeComponent } from './features/public/home/home.component';
import { ProductDetailsComponent } from './features/public/product-details/product-details.component';
import { LoginComponent } from './features/auth/login/login.component';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';
import { RegisterComponent } from './features/auth/register/register/register.component';
import { UsersListComponent } from './features/users/users-list/users-list.component';
import { EditUserComponent } from './features/users/edit-user/edit-user.component';
import { BasketComponent } from './features/blog-post/basket/basket/basket.component';
import { ReceiptComponent } from './features/blog-post/receipt/receipt/receipt.component';
import { OrderDetailComponent } from './features/blog-post/order/order.component';
import { EditOrderComponent } from './features/order/edit-order/edit-order.component';
import { OrderListComponent } from './features/order/order-list/order-list.component';
@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    CategoryListComponent,
    AddCategoryComponent,
    EditCategoryComponent,
    ProductListComponent,
    AddProductComponent,
    EditProductComponent,
    ImageSelectorComponent,
    HomeComponent,
    ProductDetailsComponent,
    LoginComponent,
    RegisterComponent,
    UsersListComponent,
    EditUserComponent,
    BasketComponent,
    ReceiptComponent,
    OrderListComponent,
    EditOrderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    MarkdownModule.forRoot()
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
