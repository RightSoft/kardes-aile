import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {SearchSupporterModel} from "@appModule/models/search-supporter.model";
import {PagedResultModel} from "@appModule/models/paged-result.model";
import {SupporterSearchResultModel} from "@appModule/models/supporter-search-result.model";
import {CreateSupporterModel} from "@appModule/models/create-supporter.model";

@Injectable({
  providedIn: 'root'
})
export class VoluntarilyService {

  private apiUrl = "api/Supporter";

  constructor(private http: HttpClient) { }

  public search(searchSupporterModel: SearchSupporterModel) {
    return this.http.post<PagedResultModel<SupporterSearchResultModel>>(`${this.apiUrl}/search`, searchSupporterModel, { responseType: "json" });
  }

  public create(createSupporterModel: CreateSupporterModel) {
    return this.http.post(`${this.apiUrl}/create`, createSupporterModel, { responseType: "json" });
  }
}
