import { Component, OnInit, Input } from '@angular/core';
import { IWorkingGroup } from '../../models/workingGroup';
import { MatDialog } from '@angular/material/dialog';
import { CreateWorkEntryComponent } from '../create-work-entry/create-work-entry.component';

@Component({
  selector: 'app-manage-working-schedules',
  templateUrl: './manage-working-schedules.component.html',
  styleUrls: ['./manage-working-schedules.component.css']
})
export class ManageWorkingSchedulesComponent implements OnInit {
  @Input() workingGroup: IWorkingGroup;

  constructor(private dialog: MatDialog) { }

  ngOnInit() {
  }

  openNewWorkEntryDialog(workingGroupScheduleId: number) {
    this.dialog.open(CreateWorkEntryComponent, {
      data: {
        workingGroup: this.workingGroup,
        workingGroupScheduleId: workingGroupScheduleId
      }
    });
  }
}
