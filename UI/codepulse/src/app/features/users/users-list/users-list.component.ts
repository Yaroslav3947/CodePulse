import { Component, OnInit } from '@angular/core';
import { UsersService } from '../services/users.service';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';

@Component({
  selector: 'app-users-list',
  // standalone: true,
  // imports: [],
  templateUrl: './users-list.component.html',
  styleUrl: './users-list.component.css'
})
export class UsersListComponent implements OnInit {

  user$?: Observable<User[]>;

  constructor(private usersService: UsersService) {
  }

  ngOnInit(): void {
    // this.usersService.getAllUsers().subscribe(
    //     data => console.log('Users data:', data),
    //     error => console.error('Error fetching users:', error)
    // );

    this.user$ = this.usersService.getAllUsers();
  }
}
