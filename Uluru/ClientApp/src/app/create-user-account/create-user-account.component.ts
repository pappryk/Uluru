import { Component, OnInit, Inject } from '@angular/core';
import { CookieHelper } from '../../helpers/cookie.helper';
import { HttpClient } from '@angular/common/http';
import { DataSharingService } from '../../helpers/datasharing.service';
import { IPosition } from '../../models/position';

@Component({
  selector: 'app-create-user-account',
  templateUrl: './create-user-account.component.html',
  styleUrls: ['./create-user-account.component.css']
})
export class CreateUserAccountComponent implements OnInit {

  private firstName: string;
  private lastName: string;
  private password: string;
  private email: string;
  private positions: IPosition[];

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private cookieHelper: CookieHelper,
    private dataSharingService: DataSharingService
  ) { }

  ngOnInit() {
    //let id = this.cookieHelper.getCookie("Id");
    this.http.get<IPosition[]>(this.baseUrl + 'api/position/groupsPositions').subscribe(result => {
      this.positions = result
    }, error => console.error(error));
  }

  async onClick() {
    console.log(this.positions);
  }
}
