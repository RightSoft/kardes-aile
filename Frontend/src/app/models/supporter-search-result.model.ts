import {UserStatuses} from "@appModule/models/shared/user-statuses.enum";

export class SupporterSearchResultModel {
  public id : string;
  public firstName : string;
  public lastName : string;
  public phone : string;
  public email : string;
  public address : string;
  public countryId: number;
  public countryName : string;
  public cityId: number;
  public cityName : string;
  public status : UserStatuses;
  public createdAt: Date;
  public matchingStatus: string;
}
