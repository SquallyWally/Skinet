export interface Pagination<T> {
  pageNumber: number;
  pageSize: number;
  count: number;
  data: T;
}
