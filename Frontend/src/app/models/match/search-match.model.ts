import {PagedSearchModel} from "@appModule/models/shared/paged-search.model";

export class SearchMatchModel extends PagedSearchModel {
  public includePassives: boolean;
  public filter: string;

  constructor(page: number, pageSize: number) {
    super();
    this.page = page;
    this.pageSize = pageSize;
    this.sortModels = [];
    this.includePassives = false;
  }
}
