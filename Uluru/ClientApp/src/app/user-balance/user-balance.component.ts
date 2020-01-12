import { Component, OnInit, Input } from '@angular/core';
import { IUser } from '../user/User';

@Component({
  selector: 'app-user-balance',
  templateUrl: './user-balance.component.html',
  styleUrls: ['./user-balance.component.css']
})
export class UserBalanceComponent implements OnInit {
  @Input() user: IUser;
  payForSchedules;

  constructor() { }

  ngOnInit() {

  }

}
