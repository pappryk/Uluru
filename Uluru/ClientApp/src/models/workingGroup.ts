import { IWorkingGroupSchedule } from "./WorkingGroupSchedule";
import { IUser } from "../app/user/User";
import { IPosition } from "./position";

export interface IWorkingGroup {
  id: number;
  name: string;
  workingGroupSchedules: IWorkingGroupSchedule[];
  users: IUser[];
  positions: IPosition[];

}
