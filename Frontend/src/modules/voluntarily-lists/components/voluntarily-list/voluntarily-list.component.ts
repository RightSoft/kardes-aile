import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ListToolBoxComponent} from '@sharedComponents/list-tool-box/list-tool-box.component';
import BaseListComponent from '@appModule/base-classes/base-list-component.abstract.class';
import {MatTableDataSource, MatTableModule} from "@angular/material/table";
import {MatPaginator, MatPaginatorModule, PageEvent} from "@angular/material/paginator";
import {PagedResultModel} from "@appModule/models/shared/paged-result.model";
import {SupporterSearchResultModel} from "@appModule/models/supporter/supporter-search-result.model";
import {SearchSupporterModel} from "@appModule/models/supporter/search-supporter.model";
import {VoluntarilyService} from "@voluntarilyListsModule/business/voluntarily.service";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {MatSortModule, Sort} from "@angular/material/sort";
import {SortDirection} from "@appModule/models/shared/sort-direction.enum";
import {SearchSortModel} from "@appModule/models/shared/search-sort.model";
import {NavigationService} from "@appModule/services/navigation.service";
import {UserStatuses, UserStatusesLabel} from "@appModule/models/shared/user-statuses.enum";
import {
  ConfirmationDialogComponent
} from "@sharedComponents/confirmation-dialog/components/confirmation-dialog.component";
import {catchError, of, switchMap, tap} from "rxjs";
import {MatDialog, MatDialogModule} from "@angular/material/dialog";
import {SnackbarService} from "@appModule/services/snackbar.service";
import {FlexModule} from "@angular/flex-layout";

@Component({
  selector: 'app-voluntarily-list',
  standalone: true,
  imports: [CommonModule, ListToolBoxComponent, MatTableModule, MatPaginatorModule, MatButtonModule, MatIconModule, MatSortModule, MatDialogModule, FlexModule],
  templateUrl: './voluntarily-list.component.html',
  styleUrls: ['./voluntarily-list.component.scss']
})
export default class VoluntarilyListComponent extends BaseListComponent implements AfterViewInit {
  displayedColumns: string[] = ['validations', 'fullName', 'cityCountyName', 'matchingStatus', 'createdAt', 'status', 'actions'];
  dataSource: MatTableDataSource<SupporterSearchResultModel> = new MatTableDataSource<SupporterSearchResultModel>();
  pagedSupporterData: PagedResultModel<SupporterSearchResultModel> = new PagedResultModel<SupporterSearchResultModel>();
  searchSupporterData: SearchSupporterModel = new SearchSupporterModel(1, 10);
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private voluntarilyService: VoluntarilyService,
              private navigationService: NavigationService,
              private dialog: MatDialog,
              private snackbar: SnackbarService) {
    super('Gönüllü Listesi');
    this.onSearch();
  }

  ngAfterViewInit() {
    this.dataSource = new MatTableDataSource<SupporterSearchResultModel>([]);
    this.dataSource.paginator = this.paginator;
  }

  changePaging($event: PageEvent) {
    this.searchSupporterData.page = $event.pageIndex;
    this.onSearch();
  }

  onSorting($event: Sort) {
    const sortDirection = $event.direction == 'desc' ? SortDirection.Descending : SortDirection.Ascending;
    this.searchSupporterData.sortModels = [];
    if ($event.active == 'fullName') {
      this.searchSupporterData.sortModels.push({
        sortName: 'firstName',
        sortDirection: sortDirection
      } as SearchSortModel);
      this.searchSupporterData.sortModels.push({sortName: 'lastName', sortDirection: sortDirection} as SearchSortModel);
    } else if ($event.active == 'cityCountryName') {
      this.searchSupporterData.sortModels.push({sortName: 'cityName', sortDirection: sortDirection} as SearchSortModel);
      this.searchSupporterData.sortModels.push({
        sortName: 'countryName',
        sortDirection: sortDirection
      } as SearchSortModel);
    } else {
      this.searchSupporterData.sortModels.push({
        sortName: $event.active,
        sortDirection: sortDirection
      } as SearchSortModel);
    }
    this.onSearch();
  }

  override onSearch(keyword?: string) {
    if (keyword !== undefined)
      this.searchSupporterData.keyword = keyword;
    this.searchSupporterData.includeDeleted = this.isChecked;

    this.voluntarilyService.search(this.searchSupporterData).subscribe((result) => {
      this.dataSource = new MatTableDataSource<SupporterSearchResultModel>(result.list);
      this.pagedSupporterData = result;
    });
  }

  onEdit(id: string) {
    this.navigationService.navigate(`/voluntarily/${id}`);
  }

  onDelete(userId: string) {
    this.dialog
      .open(ConfirmationDialogComponent, {
        width: '300px',
        data: {
          title: 'Uyarı',
          message: 'Kaydı silmek istediğinize emin misiniz?'
        }
      })
      .afterClosed()
      .pipe(
        switchMap(() => this.voluntarilyService.delete(userId)),
        tap(() => this.snackbar.show('Success', 'Successfully deleted')),
        tap(() => this.onSearch()),
        catchError((error) => {
          return of(error);
        })
      )
      .subscribe((data) => {
        if (data && data.ok === false) {
          this.snackbar.show('Error', data.message);
        }
      });
  }

  getUserStatusLabel(status: UserStatuses) {
    return UserStatusesLabel.get(status);
  }

  getCityCountryName(row: SupporterSearchResultModel) {
    return [row.cityName, row.countryName].filter(Boolean).join('/');
  }

  override onCheckboxChange(isChecked: boolean) {
    super.onCheckboxChange(isChecked);
    this.onSearch();
  }
}
