import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { UsersService } from '../services/users.service';
import { UpdateUserRequest } from '../models/update-user-request';
import { User } from '../models/user.model';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit, OnDestroy {

  id: string | null = null;

  paramsSubscription?: Subscription;
  editUserSubscription?: Subscription;
  user?: User;

    constructor(private route: ActivatedRoute,
      private usersService: UsersService,
      private router: Router) {
      
    }

  ngOnInit(): void {
    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        if(this.id) {
          // get data from API by id
          this.usersService.getUserById(this.id)
          .subscribe({
            next: (response) => {
            this.user = response;
            }
          });
        }
      }
    });
  }

  onFormSubmit(): void {
    const updateUserRequest: UpdateUserRequest = {
      email: this.user?.email ?? ' ',
      username: this.user?.userName ?? ' ',
      phoneNumber: this.user?.phoneNumber ?? ' ',
      twoFactorEnabled: this.user?.twoFactorEnabled ?? ' ',
    };

    // pass this object to service
    if(this.id) {
      this.usersService.updateUser(this.id, updateUserRequest)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/users')
        }
      });
    }
  }

  onDelete(): void {
    if(this.id) {

      // confirm deletion
      this.usersService.deleteUser(this.id)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/users')
        }
      });
    }
  }

  ngOnDestroy(): void {
      this.paramsSubscription?.unsubscribe;
      this.editUserSubscription?.unsubscribe;
  }
}
