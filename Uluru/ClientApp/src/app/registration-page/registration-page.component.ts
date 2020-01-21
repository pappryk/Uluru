// ts-nocheck
// @ts-ignore
import { Component, Inject } from '@angular/core';
// @ts-ignore
import { HttpClient } from '@angular/common/http';
// @ts-ignore
import { catchError, map, tap } from 'rxjs/operators';
// @ts-ignore
import { Observable, throwError } from 'rxjs';
import { IUser } from '../user/User';
import { IUserService, UserService } from '../user/user.service';
import { IWorkingGroup } from '../../models/workingGroup';
// @ts-ignore
import { identifierModuleUrl } from '@angular/compiler';
import { IPosition } from '../../models/position';

// @ts-ignore
@Component({
  selector: 'registration-page',
  templateUrl: './registration-page.component.html',
})
  // @ts-ignore
export class RegistrationPageComponent {
  public firstName: string;
  public lastName: string;
  public password: string;
  public email: string;
  public workingGroupName: string;

  constructor(private http: HttpClient, private userService: UserService) {}
  async onClick() {
    
    let baseUrl = document.getElementsByTagName('base')[0].href;

    if (this.firstName == undefined || this.firstName == undefined || this.lastName == undefined || this.password == undefined || this.email == undefined || this.workingGroupName == undefined) {
      alert("At least one field is empty!");
      return;
    }

    if (this.firstName == "" || this.firstName == "" || this.lastName == "" || this.password == "" || this.email == "" || this.workingGroupName == "") {
      alert("At least one field is empty!");
      return;
    }

    let workingGroup = {
      name: this.workingGroupName
    }

    let workingGroupResponse;
    try {
      workingGroupResponse = await this.http.post<IWorkingGroup>(baseUrl + 'api/workingGroup', workingGroup).toPromise();
    }
    catch (err) {
      alert("Error while creating working group!");
      return;
    }

// @ts-ignore
    let user: IUser = {
      firstName: this.firstName,
      lastName: this.lastName,
      password: this.password,
      email: this.email,
      workingGroupId: workingGroupResponse.id,
      positionId: null,
      position: null,
      userRole: "GroupAdmin",

    }

    let response: Observable<IUser> = this.userService.postUser(baseUrl + 'api/users', user);
    response.subscribe(r => {
      this.firstName = "";
      this.lastName = "";
      this.password = "";
      this.email = "";
      this.workingGroupName = "";

      alert("Registered successfully!");
    });
    response.pipe(this.handleError);
  }

  private handleError(error: any) {
    if (error.status == 409) console.log("409");
    console.error(error);
    return throwError(error);
  }
}
