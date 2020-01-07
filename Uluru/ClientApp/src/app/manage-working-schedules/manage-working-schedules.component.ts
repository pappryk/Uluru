import { Component, OnInit, Input } from '@angular/core';
import { IWorkingGroup } from '../../models/workingGroup';
import { MatDialog } from '@angular/material/dialog';
import { WorkingGroupScheduleComponent } from '../working-group-schedule/working-group-schedule.component';

@Component({
  selector: 'app-manage-working-schedules',
  templateUrl: './manage-working-schedules.component.html',
  styleUrls: ['./manage-working-schedules.component.css']
})
export class ManageWorkingSchedulesComponent implements OnInit {
  @Input() workingGroup: IWorkingGroup;

  constructor(private dialog: MatDialog) { }

  ngOnInit() {
    //console.log(this.workingGroup.workingGroupSchedules[0].workingGroupId);
    //this.dialog.open();
  }

}
