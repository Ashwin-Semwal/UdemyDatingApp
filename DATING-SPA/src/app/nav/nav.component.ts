import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { ErrorInterceptor } from '../_services/error.interceptor';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  
  constructor(public authService: AuthService, private alertify: AlertifyService) {}

  ngOnInit() {}

  login() {
    this.authService.login(this.model).subscribe(
      response => {
        this.alertify.success('Login Succesfull');
      },
      error => {
        this.alertify.error(error);
      }
    );
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  loggedOut() {
    localStorage.removeItem('token');
    this.model = {};
    this.alertify.success('Logged Out');
  }
}
