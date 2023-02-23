import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CreateSupporterModel } from '@appModule/models/supporter/create-supporter.model';

@Injectable({
  providedIn: 'root'
})
export class PublicSupporterService {

  private apiUrl = 'api/publicSupporter';

  constructor(private http: HttpClient) {
  }

  public create(createSupporterModel: CreateSupporterModel) {
    return this.http.post(`${this.apiUrl}/create`, createSupporterModel, { responseType: 'json' });
  }
}
