import { BedDouble, Calendar, FileText, Users } from "lucide-angular";
import { IModule } from "../Interfaces/_Common/IModule";

export const moduleArray: IModule[] = [
    {Title: "Room", Icon: BedDouble, Navigator: "/Rooms"},
    {Title: "Reservation", Icon: Calendar, Navigator: "/Reservations"},
    {Title: "Report", Icon: FileText, Navigator: "/Reports"},
    {Title: "User", Icon: Users, Navigator: "/Users"},
]