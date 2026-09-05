import { Component, OnInit } from '@angular/core';
import { UserGridComponent } from "../UserGrid/UserGrid.component";

@Component({
  selector: 'app-Users',
  templateUrl: './Users.component.html',
  styleUrls: ['./Users.component.css'],
  imports: [UserGridComponent]
})
export class UsersComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
