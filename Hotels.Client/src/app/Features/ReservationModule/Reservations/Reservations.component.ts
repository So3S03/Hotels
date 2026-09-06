import { Component, OnInit } from '@angular/core';
import { ReservationGridComponent } from "../ReservationGrid/ReservationGrid.component";
import { LucideAngularModule, Plus } from "lucide-angular";

@Component({
  selector: 'app-Reservations',
  templateUrl: './Reservations.component.html',
  styleUrls: ['./Reservations.component.css'],
  imports: [ReservationGridComponent, LucideAngularModule]
})
export class ReservationsComponent implements OnInit {

  icons = {
    Plus
  }

  ngOnInit() {
  }

}
