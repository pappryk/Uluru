import { Component } from '@angular/core';
import { CookieHelper } from '../../helpers/cookie.helper';
import { DataSharingService } from '../../helpers/datasharing.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
    isExpanded = false;
    isAuthenticated = false;
  constructor(
    private cookieHelper: CookieHelper,
    private dataSharingService: DataSharingService
  ) {
    this.dataSharingService.isUserLoggedIn.subscribe(value => {
      this.isAuthenticated = value;
    });
    
}
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
    }

  onClick() {
    let baseUrl = document.getElementsByTagName('base')[0].href;
    this.isAuthenticated = true;

    fetch(baseUrl + 'api/users')
      .then(result => console.log(result.ok));
  }

  logout() {
    let baseUrl = document.getElementsByTagName('base')[0].href;

    fetch(baseUrl + 'api/users/logout')
      .then(response => {
        location.href = "";
      });
  }
}
