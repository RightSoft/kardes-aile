import { environment } from './../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DisasterVictimService {
  private http = inject(HttpClient);
  private api = environment.baseUrl;
  searchDisasterVictim(
    page: number,
    pageSize: number,
    includeDeleted: boolean
  ) {
    return this.http.post(`${this.api}/api/DisasterVictim/Search`, {
      page,
      pageSize,
      includeDeleted
    });
  }
}
