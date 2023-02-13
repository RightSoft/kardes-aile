import {UserStatuses} from "@appModule/models/shared/user-statuses.enum";

export class UpdateSupporterModel {
  public userId: string;
  public firstName: string;
  public lastName: string;
  public phone: string;
  public email: string;
  public address: string;
  public cityId: string;
  public countryId: string;
  public status: UserStatuses;
}
