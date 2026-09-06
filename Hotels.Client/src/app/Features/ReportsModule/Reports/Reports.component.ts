import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-Reports',
  templateUrl: './Reports.component.html',
  styleUrls: ['./Reports.component.css'],
  imports: [RouterOutlet, RouterLinkActive, RouterLink]
})
export class ReportsComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
