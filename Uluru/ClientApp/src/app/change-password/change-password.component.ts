import { Component, OnInit, Inject } from '@angular/core';
import { IUser } from '../user/User';
import { HttpClient } from '@angular/common/http';
import { CookieHelper } from '../../helpers/cookie.helper';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  public oldPassword: string;
  public newPassword: string;
  public repeatNewPassword: string;
  public showError;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private cookieHelper: CookieHelper,
  ) { }

  ngOnInit() {
  }

  changePassword() {
    if (this.newPassword != this.repeatNewPassword) {
      alert("Repeat your password correctly!");
      return
    }
    if (this.newPassword.length < 3) {
      alert("New password must consist of at least 3 characters!")
    }

    let data = {
      oldPassword: this.oldPassword,
      newPassword: this.newPassword
    }

    this.http.put(this.baseUrl + "api/users/changePassword/" + this.cookieHelper.getCookie("Id"), data).subscribe(result => {
      alert("Password changed successfully!");
    }, error => {
      alert("Cannot change password!");
    });

    this.resetValues();
  }

  private resetValues() {
    this.newPassword = "";
    this.oldPassword = "";
    this.repeatNewPassword = "";
  }
}
