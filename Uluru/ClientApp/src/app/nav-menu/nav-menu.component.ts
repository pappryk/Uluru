import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
    isExpanded = false;
    isAuthenticated = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
    }

  onClick() {
      let baseUrl = document.getElementsByTagName('base')[0].href;
      fetch(baseUrl + 'api/users')
        .then(result => console.log(JSON.stringify(result)));
      this.isAuthenticated = true;
  }
}
