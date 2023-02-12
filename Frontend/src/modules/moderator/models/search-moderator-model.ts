import { PagedSearchModel } from "../../../models/paged-search-model";

export class SearchModeratorModel extends PagedSearchModel {
  public query?: string;
  public includeDeleted: boolean;
}
