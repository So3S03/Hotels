export interface IReservationToReturnDto {
    id: string;
    guestName: string;
    checkInDate: string;
    checkOutDate: string;
    totalAmount: number;
    statusName: string;
    statusId: number;
    roomNumber: number;
    roomId : string;
}
