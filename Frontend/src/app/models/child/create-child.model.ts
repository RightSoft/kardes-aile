import {Genders} from "@appModule/models/shared/genders.enum";

export class CreateChildModel
{
  public name: string;
  public birthDate: Date;
  public gender: Genders;
}
