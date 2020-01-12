import { Component, OnInit, Input, Inject } from '@angular/core';
import { IWorkingGroup } from '../../models/workingGroup';
import { MatDialog } from '@angular/material/dialog';
import { CreateWorkEntryComponent } from '../create-work-entry/create-work-entry.component';
import { HttpClient } from '@angular/common/http';
import { error } from 'protractor';
import { Router } from '@angular/router';
import { EditWorkEntryComponent } from '../edit-work-entry/edit-work-entry.component';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-manage-working-schedules',
  templateUrl: './manage-working-schedules.component.html',
  styleUrls: ['./manage-working-schedules.component.css'],
  providers: [DatePipe]
})
export class ManageWorkingSchedulesComponent implements OnInit {
  @Input() workingGroup: IWorkingGroup;
  private workingSchedulesGrouped;
  private newScheduleDates;


  constructor(
    private dialog: MatDialog,
    private http: HttpClient,
    private router: Router,
    private datePipe: DatePipe,
    @Inject('BASE_URL') private baseUrl: string,
  ) { }

  ngOnInit() {
    this.generateGroupedWorkEntriesProperty();
  }

  openNewWorkEntryDialog(workingGroupScheduleId: number) {
    let dialogRef = this.dialog.open(CreateWorkEntryComponent, {
      data: {
        workingGroup: this.workingGroup,
        workingGroupScheduleId: workingGroupScheduleId
      }
    });

    dialogRef.afterClosed().subscribe(d => {
      if (d) {
        //this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        //  this.router.navigate(['group']);
        //});
      }
    });
  }

  openEditWorkEntryDialog(workEntryId: number) {
    let dialogRef = this.dialog.open(EditWorkEntryComponent, {
      data: {
        workingGroup: this.workingGroup,
        workEntryId: workEntryId
      }
    });
  }

  deleteWorkEntry(workEntryId: number) {
    this.http.delete(this.baseUrl + "api/workEntry/" + workEntryId)
      .subscribe(response => {

      },
      error => {
        alert("Cannot remove");
        });

    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate(['group']);
    }); 
  }

  generateWorkingGroupSchedule(workingGroupScheduleId: number) {
    this.http.get(this.baseUrl + "api/workinggroupschedule/generate/" + workingGroupScheduleId).subscribe(result => {
      alert("Successfully generated schedule!");
    }, error => { alert("Error while generating schedule") });
  }

  private generateGroupedWorkEntriesProperty() {
    let schedules = [];

    for (let schedule of this.workingGroup.workingGroupSchedules) {

      let groupedByDate = schedule.workEntries.reduce((r, workEntry) => {
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
      schedule.workEntriesGrouped = workEntriesGrouped;
      schedules.push(workEntriesGrouped);
    }
  }

  async createNewSchedule() {
    if (this.newScheduleDates === undefined || this.newScheduleDates.includes(null)) {
      alert("Please, choose dates!");
      console.log(this.newScheduleDates);
      return;
    }

    let data = {
      workingGroupId: this.workingGroup.id,
      start: this.newScheduleDates[0],
      end: this.newScheduleDates[1]
    }

    try {
      let response = this.http.post(this.baseUrl + 'api/workingGroupSchedule', data).toPromise();
    }
    catch (err) {
      alert("Cannot create a new schedule!");
    }
  }
}
