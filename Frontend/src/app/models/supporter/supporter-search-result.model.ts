import {UserStatuses} from "@appModule/models/shared/user-statuses.enum";
import {ChildResultModel} from "@appModule/models/child/child-result.model";

export class SupporterSearchResultModel {
  public supporterId: string;
  public firstName: string;
  public lastName: string;
  public phone: string;
  public email: string;
  public address: string;
  public countryId: string;
  public countryName: string;
  public cityId: string;
  public cityName: string;
  public status: UserStatuses;
  public createdAt: Date;
  public matchingStatus: string;
  public emailValidated: boolean;
  public phoneValidated: boolean;
  public children: ChildResultModel[];
}
