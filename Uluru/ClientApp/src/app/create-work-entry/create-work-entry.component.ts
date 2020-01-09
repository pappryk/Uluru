import { Component, OnInit, Input, Inject } from '@angular/core';
import { IWorkingGroup } from '../../models/workingGroup';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { IPosition } from '../../models/position';

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
    private datePipe: DatePipe,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string 
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
      end: end,
      positionId: this.selectedPositionId,
    }
    
    let entries = [];

    for (let i = 0; i < this.quantity; i++) {
      entries.push(entry);
    }

    this.http.post(this.baseUrl + "api/workEntry", entries).subscribe(response => {
      alert("Successfully added work entry!");
    }, error => { alert("Cannot add work entry") });
  }
}
