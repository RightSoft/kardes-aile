import {Genders} from "@appModule/models/shared/genders.enum";

export class ChildResultModel {
  public id: string;
  public name: string;
  public birthDate: Date;
  public gender: Genders;
}
