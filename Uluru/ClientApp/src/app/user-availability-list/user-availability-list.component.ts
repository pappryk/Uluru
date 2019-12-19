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
  private workingAvailabilities: IUserAvailability[];
  private minimalDate = new Date();
  private newDayDate: Date = new Date();
  private newDayStartTime: Date = new Date();
  private newDayEndTime: Date = new Date();
  @Input() private showAddNewAvailability: boolean = false;


  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private cookieHelper: CookieHelper,
    private datePipe: DatePipe
  ) {

  }

  ngOnInit() {
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
    console.log(day + "T" + startHour);
    let start = new Date(day + "T" + startHour + ":00");
    let end = new Date(day + "T" + endHour + ":00");
    let userId = this.cookieHelper.getCookie("Id");

    let data = {
      userId: userId,
      start: start,
      end: end
    }

    console.log(data);

    let response = await this.http.post(this.baseUrl + 'api/workingAvailability', data, { observe: 'response' }).toPromise();
    console.log(response.ok);
  }




}
