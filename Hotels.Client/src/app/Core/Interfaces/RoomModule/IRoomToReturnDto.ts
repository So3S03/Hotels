import { IRoomReservationDto } from "./IRoomReservationDto";

export interface IRoomToReturnDto {
    id: string;
    roomNumber: number;
    roomTypeId: string;
    roomTypeName: string;
    pricePerNight: number;
    isAvailable: boolean;
    reservations: IRoomReservationDto[]
}
