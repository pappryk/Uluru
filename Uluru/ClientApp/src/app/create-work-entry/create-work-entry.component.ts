import { Component, OnInit, Input, Inject } from '@angular/core';
import { IWorkingGroup } from '../../models/workingGroup';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-create-work-entry',
  templateUrl: './create-work-entry.component.html',
  styleUrls: ['./create-work-entry.component.css'],
  providers: [DatePipe]
})
export class CreateWorkEntryComponent implements OnInit {
  @Input() workingGroup: IWorkingGroup;
  private workingGroupScheduleId;

  private selectedPositionId: number;

  private newDayDate: Date = new Date();
  private newDayStartTime: Date = new Date();
  private newDayEndTime: Date = new Date();
  private quantity: number = 1;

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: any,
    private datePipe: DatePipe
  ) {
    this.workingGroup = data.workingGroup;
    this.workingGroupScheduleId = data.workingGroupScheduleId;
  }

  ngOnInit() {
  }

  postNewWorkEntries() {
    let day = this.datePipe.transform(this.newDayDate, "yyyy-MM-dd");
    let startHour = this.datePipe.transform(this.newDayStartTime, "HH:mm");
    let endHour = this.datePipe.transform(this.newDayEndTime, "HH:mm");
    let start = new Date(day + "T" + startHour + ":00");
    let end = new Date(day + "T" + endHour + ":00");

    let entry =  {
      workingGroupScheduleId: this.workingGroupScheduleId,
      start: start,
      end: end
    }
    
    let data = [];

    for (let i = 0; i < this.quantity; i++) {
      data.push(entry);
}

    console.log(data);
    console.log(this.workingGroup);
  }
}
