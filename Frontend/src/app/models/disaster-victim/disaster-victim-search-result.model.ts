import {ChildResultModel} from "@appModule/models/child/child-result.model";

export class DisasterVictimSearchResultModel {
  id: string;
  userId: string;
  firstName: string;
  lastName: string;
  phone: string;
  email: string;
  address: string;
  cityId: string;
  temporaryCityId: string
  cityName: string;
  temporaryCityName: string;
  countryId: string;
  countryName: string;
  status: number;
  createdAt: string;
  identityNumber: string;
  identityNumberValidated: boolean;
  addressValidated: boolean;
  temporaryAddress: string
  children: ChildResultModel[];
}
