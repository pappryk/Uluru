import { Component, OnInit, Input, Inject } from '@angular/core';
import { IWorkingGroup } from '../../models/workingGroup';
import { MatDialog } from '@angular/material/dialog';
import { CreateWorkEntryComponent } from '../create-work-entry/create-work-entry.component';
import { HttpClient } from '@angular/common/http';
import { error } from 'protractor';
import { Router } from '@angular/router';

@Component({
  selector: 'app-manage-working-schedules',
  templateUrl: './manage-working-schedules.component.html',
  styleUrls: ['./manage-working-schedules.component.css']
})
export class ManageWorkingSchedulesComponent implements OnInit {
  @Input() workingGroup: IWorkingGroup;

  constructor(
    private dialog: MatDialog,
    private http: HttpClient,
    private router: Router,
    @Inject('BASE_URL') private baseUrl: string,
  ) { }

  ngOnInit() {
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
}
