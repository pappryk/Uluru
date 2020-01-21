import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IUser } from '../user/User';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  public user: IUser;
  public payForSchedules;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
  ) { }

  ngOnInit() {
    this.http.get<IUser>(this.baseUrl + "api/users/fromClaims").subscribe(result => {
      this.user = result;
      for (let schedule of this.user.workingGroup.workingGroupSchedules) {
        schedule.payForUser = 0.0;
        for (let workEntry of schedule.workEntries) {
          if (workEntry.userId == this.user.id) {
            let hours = new Date(workEntry.end).getHours() - new Date(workEntry.start).getHours();
            let minutes = new Date(workEntry.end).getMinutes() - new Date(workEntry.start).getMinutes();
            let timeInHours = hours + (minutes / 60);
            schedule.payForUser = schedule.payForUser + this.user.hourlyWage * timeInHours;
          }
        }

      }
    }, error => { console.log(error) });
  }

}
