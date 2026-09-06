import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { env } from '../../Environment/environment.env';
import { Observable } from 'rxjs';
import { IJwtDecodedObject } from '../../Interfaces/_Common/IJwtDecodedObject';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly _HttpClient: HttpClient = inject(HttpClient);
  private readonly baseUrl : string = env.baseApiUrl;
  private readonly controllerName: string = "Account";

  Login(credentials: any): Observable<any>
  {
    return this._HttpClient.post(`${this.baseUrl}/${this.controllerName}/Login`, credentials)
  }

  Register(data: any): Observable<any>
  {
    return this._HttpClient.post(`${this.baseUrl}/${this.controllerName}/Register`, data)
  }

  GetAllUsers(pageNum: number, pageSize: number, name: string = ""): Observable<any>
  {
    const httpParams = new HttpParams()
    .set("pageNum", pageNum)
    .set("pageSize", pageSize)
    .set("name", name);
    return this._HttpClient.get(`${this.baseUrl}/${this.controllerName}/GetAllUsers`, {params: httpParams})
  }

  ActivateDeActivateUser(userId: string, activate: boolean): Observable<any>
  {
  const userData : IJwtDecodedObject = JSON.parse(localStorage.getItem("userData") ?? "");
  const adminId = userData.UserId;
    const data = {
      userId: userId,
      adminId: adminId,
      activate: activate
    }
    return this._HttpClient.put(`${this.baseUrl}/${this.controllerName}/ActivateDeactivateUser`, data)
  }
}
