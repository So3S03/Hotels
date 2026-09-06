import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { env } from '../../Environment/environment.env';
import { IJwtDecodedObject } from '../../Interfaces/_Common/IJwtDecodedObject';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {
  private readonly _HttpClient: HttpClient = inject(HttpClient);
  private readonly baseUrl: string = env.baseApiUrl;
  private readonly controllerName: string = "Reservation";
  private readonly userData: IJwtDecodedObject = JSON.parse(localStorage.getItem("userData") ?? "");
  private readonly adminId = this.userData.UserId;

  GetAllReservation(pageNum: number, pageSize: number, startDate?: string, endDate?: string, minPrice?: number, maxPrice?: number, status?: number, guestName?:string): Observable<any> {
    const httpParams = new HttpParams()
      .set("PageNum", pageNum)
      .set("PageSize", pageSize)
      .set("StartDate", startDate ?? "")
      .set("EndDate", endDate ?? "")
      .set("StartPriceRange", minPrice ?? "")
      .set("EndPriceRange", maxPrice ?? "")
      .set("GuestName", guestName ?? "")
      .set("Status", status ?? "");
    return this._HttpClient.get(`${this.baseUrl}/${this.controllerName}/GetAllReservation`, { params: httpParams })
  }

  GetReservationsLog(pageNum: number, pageSize: number, reservationId: string): Observable<any> {
    const httpParams = new HttpParams()
      .set("PageNum", pageNum)
      .set("PageSize", pageSize)
      .set("ReservationId", reservationId);
    return this._HttpClient.get(`${this.baseUrl}/${this.controllerName}/GetReservationsLog`, { params: httpParams })
  }

  GetReservation(reservationId: string): Observable<any> {
    const httpParams = new HttpParams()
      .set("ReservationId", reservationId);
    return this._HttpClient.get(`${this.baseUrl}/${this.controllerName}/GetReservation`, { params: httpParams })
  }

  CancelReservation(reservationId: string): Observable<any> {
    const httpParams = new HttpParams()
      .set("ReservationId", reservationId);
    return this._HttpClient.put(`${this.baseUrl}/${this.controllerName}/CancelReservation`, null, { params: httpParams })
  }

  CreateReservation(data: any): Observable<any> {
    return this._HttpClient.put(`${this.baseUrl}/${this.controllerName}/CreateReservation`, data)
  }
}
