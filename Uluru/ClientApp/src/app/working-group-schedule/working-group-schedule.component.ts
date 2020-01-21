import { Component, OnInit, Input } from '@angular/core';
import { IWorkingGroupSchedule } from '../../models/WorkingGroupSchedule';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-working-group-schedule',
  templateUrl: './working-group-schedule.component.html',
  styleUrls: ['./working-group-schedule.component.css'],
  providers: [DatePipe]
})
export class WorkingGroupScheduleComponent implements OnInit {
  @Input() public workingGroupSchedule: IWorkingGroupSchedule;
  public workEntriesGrouped;

  constructor(
    private datePipe: DatePipe,
  ) { }

  ngOnInit() {
    this.workEntriesGrouped = this.getWorkEntriesGroupedByDate();
  }

  private getWorkEntriesGroupedByDate() {
    let groupedByDate = this.workingGroupSchedule.workEntries.reduce((r, workEntry) => {

      let day = this.datePipe.transform(workEntry.start, "dd.MM.yyyy");

      r[day] = [...r[day] || [], workEntry];
      return r;
    }, {});

    let workEntriesGrouped = [];

    for (let index in groupedByDate) {
      workEntriesGrouped.push({
        date: index,
        value: groupedByDate[index]
      })
    }


    return workEntriesGrouped;
  }
}
