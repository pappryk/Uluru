import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IUser } from '../user/User';
import { CookieHelper } from '../../helpers/cookie.helper';
import { IUserAvailability } from '../user-availability/UserAvailability';

@Component({
  selector: 'app-user-availability-list',
  templateUrl: './user-availability-list.component.html',
  styleUrls: ['./user-availability-list.component.css']
})
export class UserAvailabilityListComponent implements OnInit {
  private workingAvailabilities: IUserAvailability[];

  constructor(
    http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    cookieHelper: CookieHelper
  ) {
    let id = cookieHelper.getCookie("Id");
    console.log(id);
    http.get<IUserAvailability[]>(baseUrl + 'api/workingAvailability?userId=' + id).subscribe(result => {
      this.workingAvailabilities = result;
    }, error => console.error(error));
  }
  ngOnInit() {
  }

}
