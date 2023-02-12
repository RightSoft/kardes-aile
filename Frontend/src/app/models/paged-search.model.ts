import {SearchSortModel} from "@appModule/models/search-sort.model";

export class PagedSearchModel {
  public page: number;
  public pageSize: number;
  public sortModels: SearchSortModel[];
  constructor() {
    this.page = 1;
  }
}
