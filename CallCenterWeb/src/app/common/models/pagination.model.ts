import { MatPaginator, MatTableDataSource, MatSort } from "@angular/material";

export class Pagination<T> {
  dataCount: number;
  columns: string[];
  dataSource: MatTableDataSource<T>;
  isLoadingResults: boolean = true;
  paginator: MatPaginator;
  sort: MatSort;
}