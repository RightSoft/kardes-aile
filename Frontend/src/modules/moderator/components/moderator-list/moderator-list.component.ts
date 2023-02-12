import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import BaseListComponent from '@appModule/base-classes/base-list-component.abstract.class';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { ListToolBoxComponent } from '@sharedComponents/list-tool-box/list-tool-box.component';
import { MatTableModule } from '@angular/material/table';
import { SearchModeratorResult } from '@moderatorModule/models/search-moderator-result';
import { ModeratorService } from '@moderatorModule/moderator.service';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { PagedResultModel } from '@appModule/models/paged-result.model';
import { SearchModeratorModel } from '@moderatorModule/models/search-moderator-model';
import { AppConstants } from '@appModule/contants/app-constants';
import { catchError, filter, merge, of, startWith, switchMap, tap } from 'rxjs';
import { map } from 'rxjs/operators';
import { NavigationService } from '@appModule/services/navigation.service';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '@sharedComponents/confirmation-dialog/components/confirmation-dialog.component';
import { SnackbarService } from '@appModule/services/snackbar.service';

@Component({
  selector: 'app-moderator-list',
  standalone: true,
  imports: [
    CommonModule,
    ListToolBoxComponent,
    MatTableModule,
    MatPaginatorModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule
  ],
  templateUrl: './moderator-list.component.html',
  styleUrls: ['./moderator-list.component.scss']
})
export default class ModeratorListComponent
  extends BaseListComponent
  implements AfterViewInit
{
  @ViewChild(MatPaginator) paginator: MatPaginator;

  length: number;
  pageSize: number = AppConstants.DefaultPageSize;
  pageSizeOptions: number[];
  displayedColumns: string[] = [
    'name',
    'email',
    'createdAt',
    'isDeleted',
    'actions'
  ];

  dataSource: SearchModeratorResult[];

  constructor(
    private moderatorService: ModeratorService,
    private navigationService: NavigationService,
    private dialog: MatDialog,
    private snackbarService: SnackbarService
  ) {
    super('Moderatör Listesi');
    this.dataSource = [];
  }

  ngAfterViewInit() {
    merge(this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          return this.onSearch(this.keyword).pipe(
            map((data) => {
              this.setPaginationResult(data);
            }),
            catchError((error) => {
              return of(error);
            })
          );
        })
      )
      .subscribe();
  }

  onUpdate(row: SearchModeratorResult) {
    this.navigationService.navigate(`/moderator/${row.id}`);
  }

  onDelete(data: SearchModeratorResult) {
    console.log('onDelete');

    this.dialog
      .open(ConfirmationDialogComponent, {
        width: '300px',
        data: {
          title: 'Uyarı',
          message: 'Moderatörü silmek istediğinize emin misiniz?'
        }
      })
      .afterClosed()
      .pipe(
        filter((result) => {
          return result;
        }),
        switchMap(() => this.moderatorService.delete(data.id)),
        tap(() => this.snackbarService.show('Success', 'Successfully deleted')),
        catchError((error) => {
          return of(error);
        })
      )
      .subscribe((data) => {
        if (data && data.ok === false) {
          this.snackbarService.show('Error', data.message);
        }
      });
  }

  override onSearch(keyword?: string) {
    const request = new SearchModeratorModel();
    request.query = keyword;
    request.page = 1;
    request.pageSize = this.paginator.pageSize;
    request.includeDeleted = this.isChecked;
    return this.moderatorService.search(request);
  }

  private setPaginationResult(data: PagedResultModel<SearchModeratorResult>) {
    if (this.paginator.pageIndex === 0) {
      this.length = data.totalCount;
      this.pageSize = 10;
      this.pageSizeOptions = [10, 25, 50];
    }

    this.dataSource = data.list;
  }
}
