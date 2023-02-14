import {Injectable} from "@angular/core";
import {Observable, of, shareReplay} from "rxjs";
import {CountryResultModel} from "@appModule/models/country-result.model";
import {AddressService} from "@appModule/services/address.service";
import {CityResultModel} from "@appModule/models/city-result.model";

const CACHE_SIZE = 1;

@Injectable({
  providedIn: 'root'
})
export class CacheService {
  private countries$: Observable<CountryResultModel[]>;
  private cities$: Map<string, Observable<CityResultModel[]>> = new Map<string, Observable<CityResultModel[]>>();

  constructor(private addressService: AddressService) {

  }

  get countries(): Observable<Array<CountryResultModel>> {
    if (!this.countries$) {
      this.countries$ = this.addressService.countries().pipe(
        shareReplay(CACHE_SIZE)
      );
    }

    return this.countries$;
  }

  getCities(countryId: string): Observable<CityResultModel[]> {
    if (!countryId)
      return of([] as CityResultModel[]);

    if (!this.cities$.get(countryId)) {
      this.cities$.set(countryId, this.addressService.cities(countryId).pipe(
        shareReplay(CACHE_SIZE)
      ));
    }

    return this.cities$.get(countryId);
  }
}
