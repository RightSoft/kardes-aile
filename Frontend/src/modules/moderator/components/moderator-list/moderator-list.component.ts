import {
  AfterViewInit,
  Component,
  EventEmitter,
  Input,
  ViewChild
} from '@angular/core';
import { CommonModule } from '@angular/common';
import BaseListComponent from '@appModule/base-classes/base-list-component.abstract.class';
import {
  MatPaginator,
  MatPaginatorModule,
  PageEvent
} from '@angular/material/paginator';
import { ListToolBoxComponent } from '@sharedComponents/list-tool-box/list-tool-box.component';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { SearchModeratorResult } from '@moderatorModule/models/search-moderator-result';
import { ModeratorService } from '@moderatorModule/moderator.service';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { PagedResultModel } from '@appModule/models/shared/paged-result.model';
import { SearchModeratorModel } from '@moderatorModule/models/search-moderator-model';
import { AppConstants } from '@appModule/contants/app-constants';
import { catchError, filter, of, switchMap, tap } from 'rxjs';
import { NavigationService } from '@appModule/services/navigation.service';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '@sharedComponents/confirmation-dialog/components/confirmation-dialog.component';
import { SnackbarService } from '@appModule/services/snackbar.service';
import { SearchSupporterModel } from '@appModule/models/supporter/search-supporter.model';
import { MatSortModule, Sort } from '@angular/material/sort';
import { SortDirection } from '@appModule/models/shared/sort-direction.enum';
import { SearchSortModel } from '@appModule/models/shared/search-sort.model';
import {
  UserStatuses,
  UserStatusesLabel
} from '@appModule/models/shared/user-statuses.enum';

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
    MatDialogModule,
    MatSortModule
  ],
  templateUrl: './moderator-list.component.html',
  styleUrls: ['./moderator-list.component.scss']
})
export default class ModeratorListComponent
  extends BaseListComponent
  implements AfterViewInit
{
  @ViewChild(MatPaginator) paginator: MatPaginator;

  displayedColumns: string[] = [
    'fullName',
    'email',
    'createdAt',
    'status',
    'actions'
  ];

  dataSource: MatTableDataSource<SearchModeratorResult> =
    new MatTableDataSource<SearchModeratorResult>();
  pagedSupporterData: PagedResultModel<SearchModeratorResult> =
    new PagedResultModel<SearchModeratorResult>();
  searchData: SearchModeratorModel = new SearchSupporterModel(
    1,
    AppConstants.DefaultPageSize
  );
  userStatuses = UserStatuses;
  @Input() projectsNeedsRefresh = new EventEmitter<boolean>();

  constructor(
    private moderatorService: ModeratorService,
    private navigationService: NavigationService,
    private dialog: MatDialog,
    private snackbarService: SnackbarService
  ) {
    super('Moderatör Listesi');
    this.onSearch();
  }

  ngAfterViewInit() {
    this.dataSource = new MatTableDataSource<SearchModeratorResult>([]);
    this.dataSource.paginator = this.paginator;
  }

  onUpdate(row: SearchModeratorResult) {
    this.navigationService.navigate(`/moderator/${row.id}`);
  }

  onDelete(data: SearchModeratorResult) {
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
        tap(() => this.onSearch()),
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
    this.searchData.query = keyword?.trim();

    this.searchData.includeDeleted = this.isChecked;

    this.moderatorService.search(this.searchData).subscribe((result) => {
      this.dataSource = new MatTableDataSource<SearchModeratorResult>(
        result.list
      );
      this.pagedSupporterData = result;
    });
  }

  onChangePaging($event: PageEvent) {
    this.searchData.page = $event.pageIndex;
    this.onSearch();
  }

  onSorting($event: Sort) {
    const sortDirection =
      $event.direction == 'desc'
        ? SortDirection.Descending
        : SortDirection.Ascending;

    this.searchData.sortModels = [];

    if ($event.active == 'fullName') {
      this.searchData.sortModels.push({
        sortName: 'firstName',
        sortDirection: sortDirection
      } as SearchSortModel);
      this.searchData.sortModels.push({
        sortName: 'lastName',
        sortDirection: sortDirection
      } as SearchSortModel);
    } else {
      this.searchData.sortModels.push({
        sortName: $event.active,
        sortDirection: sortDirection
      } as SearchSortModel);
    }

    this.onSearch();
  }

  getStatusLabel(status: number): string {
    return UserStatusesLabel.get(status);
  }
}
