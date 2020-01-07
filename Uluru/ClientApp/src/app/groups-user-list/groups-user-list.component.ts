import { Component, OnInit, Inject, Input } from '@angular/core';
import { IUser } from '../user/User';
import { HttpClient } from '@angular/common/http';
import { IWorkingGroup } from '../../models/workingGroup';

@Component({
  selector: 'app-groups-user-list',
  templateUrl: './groups-user-list.component.html',
  styleUrls: ['./groups-user-list.component.css']
})
export class GroupsUserListComponent implements OnInit {
  private users: IUser[];
  @Input() workingGroup: IWorkingGroup;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
  ) { }

  ngOnInit() {
    this.http.get<IUser[]>(this.baseUrl + "api/users/group/" + this.workingGroup.id).subscribe(result => this.users = result);
  }
}
