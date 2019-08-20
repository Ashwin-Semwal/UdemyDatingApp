import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { ErrorInterceptor } from '../_services/error.interceptor';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(private authService: AuthService) {}

  ngOnInit() {}

  login() {
    this.authService.login(this.model).subscribe(
      response => {
        console.log('Login Succesfull');
      },
      error => {
        console.log(error);
      }
    );
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
  }

  loggedOut() {
    localStorage.removeItem('token');
    console.log('Logged Out');
  }
}
