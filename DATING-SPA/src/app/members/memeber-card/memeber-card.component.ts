import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/_models/user';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-memeber-card',
  templateUrl: './memeber-card.component.html',
  styleUrls: ['./memeber-card.component.css']
})
export class MemeberCardComponent implements OnInit {
@Input() user: User;
  constructor(private sanitizer: DomSanitizer) { }

  ngOnInit() {
  }

  

}
