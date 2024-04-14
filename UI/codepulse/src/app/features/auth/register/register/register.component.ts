import { Component } from '@angular/core';
import { RegisterRequest } from '../../models/register-request.model';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
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

    this.errorMessage = '';

      this.authService.register(this.model)
      .subscribe({
        next: (response) => {
  
          // Redirect back to home page
          this.router.navigateByUrl('/login');
        }
      });
  }

}
