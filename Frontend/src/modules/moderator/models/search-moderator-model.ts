import {PagedSearchModel} from '@appModule/models/shared/paged-search.model';

export class SearchModeratorModel extends PagedSearchModel {
  public query?: string;
  public includeDeleted: boolean;
}
