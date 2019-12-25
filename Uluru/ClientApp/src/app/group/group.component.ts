import { Component, OnInit, Inject } from '@angular/core';
import { IWorkingGroup } from '../../models/workingGroup';
import { HttpClient } from '@angular/common/http';
import { CookieHelper } from '../../helpers/cookie.helper';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css']
})
export class GroupComponent implements OnInit {
  private workingGroup: IWorkingGroup;
  private isNewScheduleFormVisible: boolean = false;
  private newScheduleDates;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private cookieHelper: CookieHelper,
  ) {

  }

  ngOnInit() {
    let id = this.cookieHelper.getCookie("Id");
    this.http.get<IWorkingGroup>(this.baseUrl + 'api/workingGroup/fromcredentials').subscribe(result => {
      this.workingGroup = result;
    }, error => console.error(error));

  }

  toggleNewScheduleForm() {
    this.isNewScheduleFormVisible = !this.isNewScheduleFormVisible;
  }

  async createNewSchedule() {
    if (this.newScheduleDates === undefined || this.newScheduleDates.includes(null)) {
      alert("Please, choose dates!");
      console.log(this.newScheduleDates);
      return;
    }

    let data = {
      workingGroupId: this.workingGroup.id,
      start: this.newScheduleDates[0],
      end: this.newScheduleDates[1]
    }

    try {
      let response = this.http.post(this.baseUrl + 'api/workingGroupSchedule', data).toPromise();
    }
    catch (err) {
      alert("Cannot create a new schedule!");
    }
  }
}
