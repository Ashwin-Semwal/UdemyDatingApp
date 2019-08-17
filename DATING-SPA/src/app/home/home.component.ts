import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  values: any;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getValue();
  }

  toggleRegisterMode() {
    this.registerMode = !this.registerMode;
  }

  getValue() {
    this.http.get('http://localhost:5000/api/values').subscribe(
      result => {
        this.values = result;
        console.log(this.values);
      },
      error => {
        console.log('Error');
      }
    );
  }

  cancelRegisterMode(registerMode: boolean) {
    this.registerMode = false;
  }
}
