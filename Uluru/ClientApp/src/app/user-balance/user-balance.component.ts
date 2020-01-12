import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-user-balance',
  templateUrl: './user-balance.component.html',
  styleUrls: ['./user-balance.component.css']
})
export class UserBalanceComponent implements OnInit {
  @Input() user;

  constructor() { }

  ngOnInit() {
  }

}
