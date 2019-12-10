import { Injectable } from '@angular/core';
import { IUser } from './User';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class UserService implements IUserService {
    constructor(private http: HttpClient) { }

    public postUser(url: string, user: IUser): Observable<IUser>{
        let data: string = JSON.stringify(user);

        let response = this.http.post<IUser>(url, user);

        return response;
    }
    public getAllUsers(): Observable<IUser[]>
    {
        let url: string = document.getElementsByTagName('base')[0].href + 'api/users';

        return this.http.get<IUser[]>(url);
    }

    public authenticateUser(email: string, password: string) : boolean {
        let user = {
            email: email,
            password: password
        }

        let url: string = document.getElementsByTagName('base')[0].href + 'api/users';
        //fetch()
        return true;
    }
}

export interface IUserService {
    postUser(url: string, data: IUser);
    getAllUsers()/*: Observable<IUser[]>*/;
}
