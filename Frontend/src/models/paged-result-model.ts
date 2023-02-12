export interface PagedResultModel<T> {
  totalCount: number;
  list: Array<T>;
}
