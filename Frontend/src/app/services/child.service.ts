import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ChildResultModel} from '@appModule/models/child/child-result.model';
import {CreateChildModel} from '@appModule/models/child/create-child.model';
import {UpdateChildModel} from "@appModule/models/child/update-child.model";

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
    console.log('its gonna create')
    return this.http.post(`${this.apiUrl}/add`, createChildModel, {responseType: "json"});
  }

  public update(updateChildModel: UpdateChildModel) {
    console.log('its gonna update')
    return this.http.put(`${this.apiUrl}/update`, updateChildModel, {responseType: "json"});
  }

  public delete(id: string) {
    return this.http.delete(`${this.apiUrl}/remove/${id}`, {responseType: "json"});
  }
}
