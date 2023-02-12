import {Genders} from "@appModule/models/shared/genders.enum";

export class CreateChildModel
{
  public Name: string;
  public birthDate: Date;
  public gender: Genders;
}
