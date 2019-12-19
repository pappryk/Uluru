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
  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private cookieHelper: CookieHelper,
  ) { }

  ngOnInit() {
    let id = this.cookieHelper.getCookie("Id");
    this.http.get<IWorkingGroup>(this.baseUrl + 'api/workingGroup/fromcredentials').subscribe(result => {
      this.workingGroup = result;
    }, error => console.error(error));

  }

}
