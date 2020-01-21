import { Component, OnInit, Inject, Input, Output } from '@angular/core';
import { IUserAvailability } from './UserAvailability';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-availability',
  templateUrl: './user-availability.component.html',
  styleUrls: ['./user-availability.component.css']
})
export class UserAvailabilityComponent implements OnInit {
  @Input() @Output() public availability: IUserAvailability;
  public showEdit: boolean = false;

  constructor(
    @Inject('BASE_URL') private baseUrl: string,
    private http: HttpClient,
    private router: Router
  ) {
  }

  ngOnInit() {
  }

  deleteAvailability() {
    this.http.delete(this.baseUrl + 'api/workingAvailability/' + this.availability.id).toPromise()
      .then(response => {
      });
  }

  editAvailability() {
    this.showEdit = !this.showEdit;
  }

  updateAvailability() {

  }

}
