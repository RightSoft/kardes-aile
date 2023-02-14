import {HttpClient} from '@angular/common/http';
import {Injectable, inject} from '@angular/core';
import {CreateDisasterVictimModel} from '@appModule/models/disaster-victim/create-disaster-victim.model';
import {UpdateDisasterVictimModel} from '@appModule/models/disaster-victim/update-disaster-victim.model';
import {DisasterVictimSearchRequestModel} from '@appModule/models/disaster-victim/disaster-victim-search-request.model';
import {DisasterVictimSearchResultModel} from '@appModule/models/disaster-victim/disaster-victim-search-result.model';
import {PagedResultModel} from '@appModule/models/shared/paged-result.model';

@Injectable({
  providedIn: 'root'
})
export class DisasterVictimService {
  private http = inject(HttpClient);
  private apiUrl = "api/DisasterVictim";

  public search(
    disasterVictimSearchRequestModel: DisasterVictimSearchRequestModel
  ) {
    return this.http.post<PagedResultModel<DisasterVictimSearchResultModel>>(`${this.apiUrl}/Search`, disasterVictimSearchRequestModel);
  }

  public get(id: string) {
    return this.http.get<DisasterVictimSearchResultModel>(`${this.apiUrl}/get/${id}`, {responseType: "json"});
  }

  public create(createDisasterVictimModel: CreateDisasterVictimModel) {
    return this.http.post(`${this.apiUrl}/create`, createDisasterVictimModel, {responseType: "json"});
  }

  public update(updateDisasterVictimModel: UpdateDisasterVictimModel) {
    return this.http.put(`${this.apiUrl}/update`, updateDisasterVictimModel, {responseType: "json"});
  }

  public delete(userId: string) {
    return this.http.delete(`${this.apiUrl}/delete/${userId}`);
  }

}
