import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { CookieHelper } from './cookie.helper';

@Injectable({
  providedIn: 'root'
})
export class DataSharingService {
  public isUserLoggedIn: BehaviorSubject<boolean>;

  constructor(
    private cookieHelper: CookieHelper
  ) {
    this.isUserLoggedIn = new BehaviorSubject<boolean>(cookieHelper.getCookie("Id") !== "");
  }
}
