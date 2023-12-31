import { Component } from '@angular/core';
import { RegisterRequest } from '../../models/register-request.model';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  // standalone: true,
  // imports: [],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  model: RegisterRequest;

  constructor(private authService: AuthService,
    private router: Router) {
    this.model = {
      email: '',
      password: '',
      repeatPassword: ''
    }
  }

  onFormSubmit(): void {
    this.authService.register(this.model)
      .subscribe({
        next: (response) => {
  
          // Redirect back to home page
          this.router.navigateByUrl('/login');
        }
      });
  }
}
