import { Component, Inject } from '@angular/core';
import { UserService } from '../user/user.service';

@Component({
    selector: 'user-login',
    templateUrl: './user-login.component.html',
})
export class UserLogin {
  public email: string;
  public password: string;
  constructor(private _userService: UserService) { }

  public async onClick() {
    let response = await this._userService.authenticateUser(this.email, this.password);
    if (!response.ok) {
      alert("Nie udało się zalogować.");
    }
    else {
      console.log(response.body);

    }
  }
}
