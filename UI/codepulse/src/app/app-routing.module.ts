import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryListComponent } from './features/category/category-list/category-list.component';
import { AddCategoryComponent } from './features/category/add-category/add-category.component';
import { EditCategoryComponent } from './features/category/edit-category/edit-category.component';
import { CosmeticListComponent } from './features/blog-post/cosmetic-list/cosmetic-list.component';
import { AddCosmeticComponent } from './features/blog-post/add-cosmetic/add-cosmetic.component';
import { EditCosmeticComponent } from './features/blog-post/edit-cosmetic/edit-cosmetic.component';
import { HomeComponent } from './features/public/home/home.component';
import { CosmeticDetailsComponent } from './features/public/cosmetic-details/cosmetic-details.component';
import { LoginComponent } from './features/auth/login/login.component';
import { authGuard } from './features/auth/guards/auth.guard';
import { RegisterComponent } from './features/auth/register/register/register.component';
import { UsersListComponent } from './features/users/users-list/users-list.component';
import { EditUserComponent } from './features/users/edit-user/edit-user.component';

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
    path: 'cosmetic/:url',
    component: CosmeticDetailsComponent
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
    path: 'admin/cosmetics',
    component: CosmeticListComponent,
    canActivate: [authGuard]
  },
  {
    path: 'admin/cosmetics/add',
    component: AddCosmeticComponent,
    canActivate: [authGuard]
  },
  {
    path: 'admin/cosmetics/:id',
    component: EditCosmeticComponent,
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
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
