import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {SearchSupporterModel} from "@appModule/models/supporter/search-supporter.model";
import {PagedResultModel} from "@appModule/models/shared/paged-result.model";
import {SupporterSearchResultModel} from "@appModule/models/supporter/supporter-search-result.model";
import {CreateSupporterModel} from "@appModule/models/supporter/create-supporter.model";
import {UpdateSupporterModel} from "@appModule/models/supporter/update-supporter.model";

@Injectable({
  providedIn: 'root'
})
export class VoluntarilyService {

  private apiUrl = "api/Supporter";

  constructor(private http: HttpClient) {
  }

  public get(id: string) {
    return this.http.get<SupporterSearchResultModel>(`${this.apiUrl}/get/${id}`, {responseType: "json"});
  }

  public search(searchSupporterModel: SearchSupporterModel) {
    return this.http.post<PagedResultModel<SupporterSearchResultModel>>(`${this.apiUrl}/search`, searchSupporterModel, {responseType: "json"});
  }

  public create(createSupporterModel: CreateSupporterModel) {
    return this.http.post(`${this.apiUrl}/create`, createSupporterModel, {responseType: "json"});
  }

  public update(updateSupporterModel: UpdateSupporterModel) {
    return this.http.put(`${this.apiUrl}/update`, updateSupporterModel);
  }

  public delete(userId: string) {
    return this.http.delete(`${this.apiUrl}/delete/${userId}`);
  }
}
