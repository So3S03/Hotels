import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { env } from '../../Environment/environment.env';
import { IJwtDecodedObject } from '../../Interfaces/_Common/IJwtDecodedObject';

@Injectable({
  providedIn: 'root'
})
export class RoomService {
  private readonly _HttpClient: HttpClient = inject(HttpClient);
  private readonly baseUrl: string = env.baseApiUrl;
  private readonly controllerName: string = "Room";
  private readonly userData: IJwtDecodedObject = JSON.parse(localStorage.getItem("userData") ?? "");
  private readonly adminId = this.userData.UserId;

  GetAllRooms(pageNum: number, pageSize: number, roomType?: number, minPrice?: number, maxPrice?: number, isAvailable?: boolean): Observable<any> {
    const httpParams = new HttpParams()
      .set("PageNum", pageNum)
      .set("PageSize", pageSize)
      .set("Type", roomType ?? "")
      .set("StartPrice", minPrice ?? "")
      .set("EndPrice", maxPrice ?? "")
      .set("isAvailable", isAvailable ?? "");
      return this._HttpClient.get(`${this.baseUrl}/${this.controllerName}/GetAllRooms`, {params: httpParams})
  }

  GetRoomLog(pageNum: number, pageSize: number, roomId: string): Observable<any> {
    const httpParams = new HttpParams()
      .set("PageNum", pageNum)
      .set("PageSize", pageSize)
      .set("RoomId", roomId ?? "");
      return this._HttpClient.get(`${this.baseUrl}/${this.controllerName}/GetRoomsLog`, {params: httpParams})
  }

  GetRoomById(roomId: string): Observable<any> {
    const httpParams = new HttpParams()
      .set("RoomId", roomId ?? "");
      return this._HttpClient.get(`${this.baseUrl}/${this.controllerName}/GetRoomById`, {params: httpParams})
  }

  DeleteRoom(roomId: string): Observable<any> {
    const httpParams = new HttpParams()
      .set("RoomId", roomId ?? "");
      return this._HttpClient.delete(`${this.baseUrl}/${this.controllerName}/DeleteRoom`, {params: httpParams})
  }

  UpdateRoom(data: any): Observable<any> {
      return this._HttpClient.put(`${this.baseUrl}/${this.controllerName}/UpdateRoom`, data)
  }

  AddRoom(data: any): Observable<any> {
      return this._HttpClient.post(`${this.baseUrl}/${this.controllerName}/AddRoom`, data)
  }
}
