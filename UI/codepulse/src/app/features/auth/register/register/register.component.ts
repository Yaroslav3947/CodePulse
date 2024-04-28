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
  errorMessage: string = '';

  constructor(private authService: AuthService,
    private router: Router) {
    this.model = {
      email: '',
      password: '',
      repeatPassword: ''
    }
  }

  onFormSubmit(): void {
    if (this.model.password !== this.model.repeatPassword) {
      this.errorMessage = "Passwords do not match";
      return; 
    }

    if (!this.model.email.trim() || !this.model.password.trim() || !this.model.repeatPassword.trim()) {
      this.errorMessage = "All fields are required";
      return; 
    }
  
    this.errorMessage = '';
  
    
    this.authService.register(this.model)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/login');
        },
        error: (err) => {
          console.log(err);
          if (err.error && err.error.errors && Array.isArray(err.error.errors[''])) {
            this.errorMessage = err.error.errors[''].join('; ');
          } else {
            this.errorMessage = "An error occurred during registration.";
          }
        }
      });
  }
}
