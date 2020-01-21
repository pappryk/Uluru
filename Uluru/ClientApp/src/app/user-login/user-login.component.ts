import { Component, Inject } from '@angular/core';
import { UserService } from '../user/user.service';
import { DataSharingService } from '../../helpers/datasharing.service';
import { Router } from '@angular/router';

@Component({
    selector: 'user-login',
    templateUrl: './user-login.component.html',
})
export class UserLogin {
  public email: string;
  public password: string;
  private isUserLoggedIn: boolean;

  constructor(
    private _userService: UserService,
    private dataSharingService: DataSharingService,
    private router: Router
  ) {
    this.dataSharingService.isUserLoggedIn.subscribe(value => {
      this.isUserLoggedIn = value;
    });
  }

  public async onClick() {
    try {
      let response = await this._userService.authenticateUser(this.email, this.password);

      this.dataSharingService.isUserLoggedIn.next(true);
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/']);
      }); 
    } catch (err) {
      alert("Cannot log in");
    }
    
  }
}
