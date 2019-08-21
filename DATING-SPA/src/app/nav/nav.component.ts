import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { ErrorInterceptor } from '../_services/error.interceptor';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  
  constructor(public authService: AuthService, private alertify: AlertifyService, private router: Router) {}

  ngOnInit() {}

  login() {
    this.authService.login(this.model).subscribe(
      response => {
        this.alertify.success('Login Succesfull');
      },
      error => {
        this.alertify.error(error);
      }, () => this.router.navigate(['/members']) // navigate to this page on loggin
    );
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  loggedOut() {
    localStorage.removeItem('token');
    this.model = {};
    this.alertify.success('Logged Out');
    this.router.navigate(['/home']);
  }
}
