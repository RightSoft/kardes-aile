import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PagedResultModel } from '../../../app/models/shared/paged-result.model';
import { AuditSearchResultModel } from '../models/audit-search-result.model';
import { AuditSearchModel } from '../models/audit-search.model';

@Injectable({
  providedIn: 'root'
})
export class AuditsService {

  constructor(
    private httpClient: HttpClient
  ) { }

  search(model: AuditSearchModel) {
    return this.httpClient.post<PagedResultModel<AuditSearchResultModel>>(`api/audit/search`, model);
  }
}
