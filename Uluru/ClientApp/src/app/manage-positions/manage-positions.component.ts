import { Component, OnInit, Inject, Input } from '@angular/core';
import { IPosition } from '../../models/position';
import { HttpClient } from '@angular/common/http';
import { IWorkingGroup } from '../../models/workingGroup';

@Component({
  selector: 'app-manage-positions',
  templateUrl: './manage-positions.component.html',
  styleUrls: ['./manage-positions.component.css']
})
export class ManagePositionsComponent implements OnInit {
  @Input() workingGroup: IWorkingGroup;
  private positions: IPosition[];
  private newPositionName: string;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
  ) { }

  ngOnInit() {
    this.http.get<IPosition[]>(this.baseUrl + 'api/position/groups/' + this.workingGroup.id).subscribe(result => {
      this.positions = result
    }, error => console.error(error));
  }

  async createPosition() {
    let data = {
      name: this.newPositionName,
      workingGroupId: this.workingGroup.id,
    }
    let response;

    try {
      this.http.post<IPosition>(this.baseUrl + 'api/position', data).subscribe(result => {
        response = result
      }, error => console.error(error));
    }
    catch (err) {
      alert("Error");
    }
  }
}
