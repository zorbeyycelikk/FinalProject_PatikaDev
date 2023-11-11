import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { StorageService } from '../services/storage.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent{

  constructor(
    private authService: AuthService,
    private router: Router,
    private storage: StorageService,
  ) {
  }

  loginForm = new FormGroup({
    email: new FormControl(''),
    password: new FormControl('')
  });

  onSubmit(event: Event) {
    event.preventDefault();
    const { email, password } = this.loginForm.value;


    this.authService.logIn(email, password).subscribe({
      next: data => {
        this.storage.saveUser(data.response)
        this.router.navigate(['/main'])
      },
      error: error => {
        this.storage.clean();
        console.log("Error", error);
      }
    })

  }
}
