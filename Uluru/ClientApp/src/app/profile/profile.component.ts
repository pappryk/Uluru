import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IUser } from '../user/User';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  private user: IUser[];

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
  ) { }

  ngOnInit() {
    this.http.get<IUser[]>(this.baseUrl + "api/users/fromClaims").subscribe(result => {
      this.user = result;
      console.log(result);
    }, error => { console.log(error) });
  }

}
