import { Component, OnInit, Input, Inject } from '@angular/core';
import { IWorkingGroup } from '../../models/workingGroup';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-edit-work-entry',
  templateUrl: './edit-work-entry.component.html',
  styleUrls: ['./edit-work-entry.component.css']
})
export class EditWorkEntryComponent implements OnInit {
  @Input() workingGroup: IWorkingGroup;
  private selectedPositionId: number;
  private selectedUserId: number;
  private workEntryId: number;

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: any,
    @Inject('BASE_URL') private baseUrl: string,
    private http: HttpClient
  ) {
    this.workingGroup = data.workingGroup;
    this.workEntryId = data.workEntryId;
  }

  ngOnInit() {
  }

  updateWorkEntry() {
    if (!this.selectedUserId)
      return;

    let requestBody = {
      id: this.workEntryId,
      userId: this.selectedUserId,
    }
    this.http.put(this.baseUrl + "api/workEntry/user", requestBody).subscribe(response => {

    }, error => { console.log(error) });
  }
}
