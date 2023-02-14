import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {PagedResultModel} from '@appModule/models/shared/paged-result.model';
import {CreateModeratorModel} from './models/create-moderator-model';
import {ModeratorResult} from './models/moderator-result';
import {SearchModeratorModel} from './models/search-moderator-model';
import {SearchModeratorResult} from './models/search-moderator-result';
import {UpdateModeratorModel} from './models/update-moderator-model';

@Injectable({
  providedIn: 'root'
})
export class ModeratorService {
  private readonly _baseUrl = `api/moderators`;

  constructor(private httpClient: HttpClient) {
  }

  get(id: string) {
    return this.httpClient.get<ModeratorResult>(`${this._baseUrl}/${id}`);
  }

  create(model: CreateModeratorModel) {
    return this.httpClient.post(`${this._baseUrl}`, model);
  }

  update(id: string, model: UpdateModeratorModel) {
    return this.httpClient.put(`${this._baseUrl}/${id}`, model);
  }

  delete(id: string) {
    return this.httpClient.delete(`${this._baseUrl}/${id}`);
  }

  search(model: SearchModeratorModel) {
    return this.httpClient.post<PagedResultModel<SearchModeratorResult>>(
      `${this._baseUrl}/search`,
      model
    );
  }
}
