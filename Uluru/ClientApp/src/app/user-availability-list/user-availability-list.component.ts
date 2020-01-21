import { Component, OnInit, Inject, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IUser } from '../user/User';
import { CookieHelper } from '../../helpers/cookie.helper';
import { IUserAvailability } from '../user-availability/UserAvailability';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-user-availability-list',
  templateUrl: './user-availability-list.component.html',
  styleUrls: ['./user-availability-list.component.css'],
  providers:[DatePipe]
})
export class UserAvailabilityListComponent implements OnInit {
  public workingAvailabilities: IUserAvailability[];
  public minimalDate = new Date();
  public newDayDate: Date = new Date();
  public newDayStartTime: Date = new Date();
  public newDayEndTime: Date = new Date();
  @Input() private showAddNewAvailability: boolean = false;


  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private cookieHelper: CookieHelper,
    private datePipe: DatePipe
  ) {

  }

  ngOnInit() {
    this.newDayStartTime.setHours(8);
    this.newDayEndTime.setHours(16);
    this.newDayStartTime.setMinutes(0);
    this.newDayEndTime.setMinutes(0);

    let id = this.cookieHelper.getCookie("Id");
    this.http.get<IUserAvailability[]>(this.baseUrl + 'api/workingAvailability?userId=' + id).subscribe(result => {
      this.workingAvailabilities = result;
    }, error => console.error(error));
  }

  toggleAvailability(): void {
    this.showAddNewAvailability = !this.showAddNewAvailability;
  }

  async postNewAvailability() {

    let day = this.datePipe.transform(this.newDayDate, "yyyy-MM-dd");
    let startHour = this.datePipe.transform(this.newDayStartTime, "HH:mm");
    let endHour = this.datePipe.transform(this.newDayEndTime, "HH:mm");
    let start = new Date(day + "T" + startHour + ":00-0000");
    let end = new Date(day + "T" + endHour + ":00-0000");
    let userId = this.cookieHelper.getCookie("Id");

    let data = {
      userId: userId,
      start: start,
      end: end
    }


    let response = await this.http.post(this.baseUrl + 'api/workingAvailability', data, { observe: 'response' }).toPromise();
  }

}
