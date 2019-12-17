import { Component, OnInit, Inject, Input, Output } from '@angular/core';
import { IUserAvailability } from './UserAvailability';

@Component({
  selector: 'app-user-availability',
  templateUrl: './user-availability.component.html',
  styleUrls: ['./user-availability.component.css']
})
export class UserAvailabilityComponent implements OnInit {
  @Input() @Output() public availability: IUserAvailability;

  constructor() {
  }

  ngOnInit() {
  }

}
