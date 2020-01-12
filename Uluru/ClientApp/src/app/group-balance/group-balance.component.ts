import { Component, OnInit, Input } from '@angular/core';
import { IWorkingGroup } from '../../models/workingGroup';

@Component({
  selector: 'app-group-balance',
  templateUrl: './group-balance.component.html',
  styleUrls: ['./group-balance.component.css']
})
export class GroupBalanceComponent implements OnInit {
  @Input() workingGroup: IWorkingGroup;
  constructor() { }

  ngOnInit() {
    for (let schedule of this.workingGroup.workingGroupSchedules) {
      schedule.costPerUser = [];
      schedule.overallCost = 0.0;

      let costsByUser = schedule.workEntries.reduce((r, workEntry) => {
        if (!workEntry.user)
          return r;

        r[workEntry.user.id] = [...r[workEntry.user.id] || [], workEntry];
        return r;
      }, {});

      for (let workEntry of schedule.workEntries) {
        let hours = new Date(workEntry.end).getHours() - new Date(workEntry.start).getHours();
        let minutes = new Date(workEntry.end).getMinutes() - new Date(workEntry.start).getMinutes();
        let timeInHours = hours + (minutes / 60);
        if (workEntry.user) {
          let workEntryCost = workEntry.user.hourlyWage * timeInHours;
          schedule.overallCost = schedule.overallCost + workEntryCost;
          let index = workEntry.user.id;
          //schedule.costPerUser[index] = schedule.costPerUser[index] ? schedule.costPerUser[index] + cost : cost;
          
          let cpu = schedule.costPerUser.find(cpu => cpu.user.id == workEntry.user.id);

          if (cpu) {
            cpu.cost += workEntryCost;
          }
          else {
            schedule.costPerUser.push({ user: workEntry.user, cost: workEntryCost });
          }
        }
        
      }
    }
  }

}
