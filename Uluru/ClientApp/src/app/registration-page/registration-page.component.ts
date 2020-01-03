import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { IUser } from '../user/User';
import { IUserService, UserService } from '../user/user.service';
import { IWorkingGroup } from '../../models/workingGroup';
import { identifierModuleUrl } from '@angular/compiler';
import { IPosition } from '../../models/position';

@Component({
  selector: 'registration-page',
  templateUrl: './registration-page.component.html',
})
export class RegistrationPageComponent {
  public firstName: string;
  public lastName: string;
  public password: string;
  public email: string;
  private workingGroupName: string;

  constructor(private http: HttpClient, private userService: UserService) {}
  async onClick() {
    
    let baseUrl = document.getElementsByTagName('base')[0].href;

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


    let user: IUser = {
      firstName: this.firstName,
      lastName: this.lastName,
      password: this.password,
      email: this.email,
      workingGroupId: workingGroupResponse.id,
      positionId: null,
      position: null,
    }

    let response: Observable<IUser> = this.userService.postUser(baseUrl + 'api/users', user);
    response.subscribe(r => {/* if (r.status == 409) console.log("409"); */});
    response.pipe(this.handleError);
  }

  private handleError(error: any) {
    if (error.status == 409) console.log("409");
    console.error(error);
    return throwError(error);
  }
}
