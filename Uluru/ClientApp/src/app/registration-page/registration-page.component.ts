import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { IUser } from '../user/User';
import { IUserService, UserService } from '../user/user.service';

@Component({
  selector: 'registration-page',
  templateUrl: './registration-page.component.html',
})
export class RegistrationPageComponent {
    public firstName: string;
    public lastName: string;
    public password: string;
    public email: string;

    constructor(private http: HttpClient, private userService: UserService) {}
    onClick(): void {
        let baseUrl = document.getElementsByTagName('base')[0].href;
        let url = baseUrl + 'api/users';

        let user: IUser = {
            firstName: this.firstName,
            lastName: this.lastName,
            password: this.password,
            email: this.email,
        }

        let response: Observable<IUser> = this.userService.postUser(url, user);
        response.subscribe(r => {/* if (r.status == 409) console.log("409"); */});
        response.pipe(this.handleError);
    }


    //private handleError<T>(operation: string, result?: T): Observable<T> {
    //    return (error: any): Observable<T> => {
    //        alert("Failed at " + operation);
    //        return new Observable<T>();
    //    }
    //}
    private handleError(error: any) {
        if (error.status == 409) console.log("409");
      console.error(error);
      return throwError(error);
  }
}
