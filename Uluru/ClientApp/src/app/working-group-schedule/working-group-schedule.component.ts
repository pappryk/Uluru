import { Component, OnInit, Input } from '@angular/core';
import { IWorkingGroupSchedule } from '../../models/WorkingGroupSchedule';

@Component({
  selector: 'app-working-group-schedule',
  templateUrl: './working-group-schedule.component.html',
  styleUrls: ['./working-group-schedule.component.css']
})
export class WorkingGroupScheduleComponent implements OnInit {
  @Input() public workingGroupSchedule: IWorkingGroupSchedule;
  private isVisible: boolean = false;

  constructor() { }

  ngOnInit() {
  }

  showComponent() {
    this.isVisible = true;
  }

  hideComponent() {
    this.isVisible = false;
  }
}
