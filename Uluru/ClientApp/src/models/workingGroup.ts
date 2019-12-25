import { IWorkingGroupSchedule } from "./WorkingGroupSchedule";
import { IUser } from "../app/user/User";

export interface IWorkingGroup {
  id: number;
  name: string;
  workingGroupSchedules: IWorkingGroupSchedule[];
  users: IUser[];
}
