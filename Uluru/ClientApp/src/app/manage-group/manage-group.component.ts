import { Component, OnInit, Input } from '@angular/core';
import { MatButtonToggle, MatButtonToggleGroup} from '@angular/material/button-toggle'
import { IWorkingGroup } from '../../models/workingGroup';

@Component({
  selector: 'app-manage-group',
  templateUrl: './manage-group.component.html',
  styleUrls: ['./manage-group.component.css']
})
export class ManageGroupComponent implements OnInit {
  @Input() workingGroup: IWorkingGroup;
  constructor() { }

  ngOnInit() {
  }

  onClick() {

  }

}
