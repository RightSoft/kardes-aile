import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CountryResultModel} from '@appModule/models/country-result.model';
import {CityResultModel} from '@appModule/models/city-result.model';

@Injectable({
  providedIn: 'root'
})
export class AddressService {

  private apiUrl = "api/Address";

  constructor(private http: HttpClient) {
  }

  public countries() {
    return this.http.get<CountryResultModel[]>(`${this.apiUrl}/getCountries`, {responseType: "json"});
  }

  public cities(countryId: string) {
    return this.http.get<CityResultModel[]>(`${this.apiUrl}/getCities?countryId=${countryId}`, {responseType: "json"});
  }
}
