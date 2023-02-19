import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CreateMatchModel} from "@appModule/models/match/create-match.model";
import {UpdateMatchModel} from "@appModule/models/match/update-match.model";
import {PagedResultModel} from "@appModule/models/shared/paged-result.model";
import {MatchSearchResultModel} from "@appModule/models/match/match-search-result.model";
import {SearchMatchModel} from "@appModule/models/match/search-match.model";

@Injectable({
  providedIn: 'root'
})
export class MatchListsService {
  private apiUrl = "api/Match";

  constructor(private http: HttpClient) {}

  public get(id: string) {
    return this.http.get<MatchSearchResultModel>(`${this.apiUrl}/get/${id}`, {responseType: "json"});
  }

  public search(searchMatchModel: SearchMatchModel) {
    return this.http.post<PagedResultModel<MatchSearchResultModel>>(`${this.apiUrl}/search`, searchMatchModel, {responseType: "json"});
  }

  public create(createMatchModel: CreateMatchModel) {
    return this.http.post(`${this.apiUrl}/create`, createMatchModel, {responseType: "json"});
  }

  public update(updateMatchModel: UpdateMatchModel) {
    return this.http.put(`${this.apiUrl}/update`, updateMatchModel);
  }

  public delete(matchId: string) {
    return this.http.delete(`${this.apiUrl}/delete/${matchId}`);
  }
}
