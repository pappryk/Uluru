import { IUser } from "../user/User";

export interface IUserAvailability {
  id: number;
  userid: number;
  user: IUser;
  start;
  end;
}
