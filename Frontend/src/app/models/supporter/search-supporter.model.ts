import {PagedSearchModel} from "@appModule/models/shared/paged-search.model";

export class SearchSupporterModel extends PagedSearchModel {
  public includeDeleted: boolean;
  public keyword: string;

  constructor(page: number, pageSize: number) {
    super();
    this.page = page;
    this.pageSize = pageSize;
    this.sortModels = [];
    this.includeDeleted = false;
  }
}
