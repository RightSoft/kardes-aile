import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ChildResultModel} from '@appModule/models/child/child-result.model';
import {CreateChildModel} from '@appModule/models/child/create-child.model';

@Injectable({
  providedIn: 'root'
})
export class ChildService {

  private apiUrl = "api/Child";

  constructor(private http: HttpClient) {
  }

  public list(userId: string) {
    return this.http.get<Array<ChildResultModel>>(`${this.apiUrl}/list/${userId}`, {responseType: "json"});
  }

  public create(createChildModel: CreateChildModel) {
    return this.http.post(`${this.apiUrl}/create`, createChildModel, {responseType: "json"});
  }

  // public update(updateSupporterModel: UpdateSupporterModel) {
  //     return this.http.put(`${this.apiUrl}/update`, updateSupporterModel);
  // }

  public delete(userId: string) {
    return this.http.delete(`${this.apiUrl}/remove?id=${userId}`, {responseType: "json"});
  }
}
