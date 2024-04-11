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
import { CosmeticListComponent } from './features/blog-post/cosmetic-list/cosmetic-list.component';
import { AddCosmeticComponent } from './features/blog-post/add-cosmetic/add-cosmetic.component';
import { MarkdownModule } from 'ngx-markdown';
import { EditCosmeticComponent } from './features/blog-post/edit-cosmetic/edit-cosmetic.component';
import { ImageSelectorComponent } from './shared/components/image-selector/image-selector.component';
import { HomeComponent } from './features/public/home/home.component';
import { CosmeticDetailsComponent } from './features/public/cosmetic-details/cosmetic-details.component';
import { LoginComponent } from './features/auth/login/login.component';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';
import { RegisterComponent } from './features/auth/register/register/register.component';
import { UsersListComponent } from './features/users/users-list/users-list.component';
import { EditUserComponent } from './features/users/edit-user/edit-user.component';
import { BasketComponent } from './features/blog-post/basket/basket/basket.component';
@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    CategoryListComponent,
    AddCategoryComponent,
    EditCategoryComponent,
    CosmeticListComponent,
    AddCosmeticComponent,
    EditCosmeticComponent,
    ImageSelectorComponent,
    HomeComponent,
    CosmeticDetailsComponent,
    LoginComponent,
    RegisterComponent,
    UsersListComponent,
    EditUserComponent,
    BasketComponent
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
