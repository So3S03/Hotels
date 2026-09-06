import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { env } from '../../Environment/environment.env';
import { IJwtDecodedObject } from '../../Interfaces/_Common/IJwtDecodedObject';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  private readonly _HttpClient: HttpClient = inject(HttpClient);
  private readonly baseUrl: string = env.baseApiUrl;
  private readonly controllerName: string = "Report";
  private readonly userData: IJwtDecodedObject = JSON.parse(localStorage.getItem("userData") ?? "");
  private readonly adminId = this.userData.UserId;

  GetTopNonCancelledRooms(pageNum: number, pageSize: number, roomType?: number): Observable<any> {
    const httpParams = new HttpParams()
      .set("PageNum", pageNum)
      .set("PageSize", pageSize)
      .set("Type", roomType ?? "");
    return this._HttpClient.get(`${this.baseUrl}/${this.controllerName}/GetTopNonCancelledRooms`, { params: httpParams })
  }

  GetRevenue(from?: string, to?: string): Observable<any> {
    const httpParams = new HttpParams()
      .set("From", from ?? "")
      .set("To", to ?? "");
    return this._HttpClient.get(`${this.baseUrl}/${this.controllerName}/GetTopNonCancelledRooms`, { params: httpParams })
  }

  GetRoomOcuupancy(pageNum: number, pageSize: number, from?: string, to?: string): Observable<any> {
    const httpParams = new HttpParams()
      .set("PageNum", pageNum)
      .set("PageSize", pageSize)
      .set("From", from ?? "")
      .set("To", to ?? "");
    return this._HttpClient.get(`${this.baseUrl}/${this.controllerName}/GetRoomOcuupancy`, { params: httpParams })
  }

}
