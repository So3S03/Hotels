import { Component, OnInit, signal, WritableSignal } from '@angular/core';
import { RoomGridComponent } from "../RoomGrid/RoomGrid.component";
import { LucideAngularModule, Plus } from "lucide-angular";
import { CreateUpdateRoomComponent } from "../CreateUpdateRoom/CreateUpdateRoom.component";
import { RoomLogComponent } from "../RoomLog/RoomLog.component";

@Component({
  selector: 'app-Rooms',
  templateUrl: './Rooms.component.html',
  styleUrls: ['./Rooms.component.css'],
  imports: [RoomGridComponent, LucideAngularModule, CreateUpdateRoomComponent, RoomLogComponent]
})
export class RoomsComponent implements OnInit {

  icons = {
    Plus
  };
  activePopUp:WritableSignal<"" | "Add" | "Update" | "Log"> = signal("");
  passedId: WritableSignal<string> = signal("");

  ngOnInit() {
  }

  openPopUp(name: "Update" | "Log", id: string) :void
  {
    this.activePopUp.set(name);
    this.passedId.set(id);
  }

}
