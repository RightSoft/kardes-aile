import {CreateChildModel} from "@appModule/models/child/create-child.model";

export class CreateSupporterModel {
  public firstName: string;
  public lastName: string;
  public phone: string;
  public email: string;
  public address: string;
  public cityId: string;
  public countryId: string;
  public children: CreateChildModel[];
}
