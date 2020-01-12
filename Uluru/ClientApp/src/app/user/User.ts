import { IPosition } from "../../models/position";

export interface IUser {
  firstName: string;
  lastName: string;
  password: string;
  email: string;
  position: IPosition;
  positionId: number;
  workingGroupId;
  workEntries;
  workingGroup;
}
