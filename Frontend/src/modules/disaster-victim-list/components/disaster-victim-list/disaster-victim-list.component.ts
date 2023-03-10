import {DisasterVictimService} from './../../business/disaster-victim.service';
import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ListToolBoxComponent} from '@sharedComponents/list-tool-box/list-tool-box.component';
import BaseListComponent from '@appModule/base-classes/base-list-component.abstract.class';
import {MatPaginator, MatPaginatorModule, PageEvent} from "@angular/material/paginator";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {MatSortModule, Sort} from "@angular/material/sort";
import {MatTableDataSource, MatTableModule} from "@angular/material/table";
import {DisasterVictimSearchResultModel} from '@appModule/models/disaster-victim/disaster-victim-search-result.model';
import {PagedResultModel} from '@appModule/models/shared/paged-result.model';
import {UserStatuses} from '@appModule/models/shared/user-statuses.enum';
import {DisasterVictimSearchRequestModel} from '@appModule/models/disaster-victim/disaster-victim-search-request.model';
import {NavigationService} from '@appModule/services/navigation.service';
import {SortDirection} from '@appModule/models/shared/sort-direction.enum';
import {SearchSortModel} from '@appModule/models/shared/search-sort.model';
import {
  ConfirmationDialogComponent
} from "@sharedComponents/confirmation-dialog/components/confirmation-dialog.component";
import {catchError, filter, of, switchMap, tap} from "rxjs";
import {SnackbarService} from "@appModule/services/snackbar.service";
import {MatDialog, MatDialogModule} from '@angular/material/dialog';

@Component({
  selector: 'app-disaster-victim-list',
  standalone: true,
  imports: [CommonModule,
    ListToolBoxComponent,
    MatPaginatorModule,
    MatButtonModule,
    MatIconModule,
    MatSortModule,
    MatTableModule,
    MatDialogModule],
  templateUrl: './disaster-victim-list.component.html',
  styleUrls: ['./disaster-victim-list.component.scss']
})
export default class DisasterVictimListComponent
  extends BaseListComponent
  implements AfterViewInit {
  displayedColumns: string[] = ['fullName', 'cityCountyName', 'matchingStatus', 'createdAt', 'status', 'actions'];
  dataSource: MatTableDataSource<DisasterVictimSearchResultModel> = new MatTableDataSource<DisasterVictimSearchResultModel>();
  pagedVictimData: PagedResultModel<DisasterVictimSearchResultModel> = new PagedResultModel<DisasterVictimSearchResultModel>();
  disasterVictimRequestModel: DisasterVictimSearchRequestModel = new DisasterVictimSearchRequestModel(1, 10);
  UserStatusesEnum = UserStatuses;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private navigationService: NavigationService,
              private disasterVictimService: DisasterVictimService,
              private snackbar: SnackbarService,
              private dialog: MatDialog) {
    super('Afetzede Listesi');
    this.onSearch();
  }

  ngAfterViewInit() {
    this.dataSource = new MatTableDataSource<DisasterVictimSearchResultModel>([]);
    this.dataSource.paginator = this.paginator;
  }

  changePaging($event: PageEvent) {
    this.disasterVictimRequestModel.page = $event.pageIndex;
    this.onSearch();
  }

  override onSearch(keyword?: string) {
    if (keyword)
      this.disasterVictimRequestModel.keyword = keyword;
    this.disasterVictimRequestModel.includeDeleted = this.isChecked;

    this.disasterVictimService.search(this.disasterVictimRequestModel).subscribe((result) => {
      this.dataSource = new MatTableDataSource<DisasterVictimSearchResultModel>(result.list);
      this.pagedVictimData = result;
    });
  }

  onSorting($event: Sort) {
    const sortDirection = $event.direction == 'desc' ? SortDirection.Descending : SortDirection.Ascending;
    this.disasterVictimRequestModel.sortModels = [];
    if ($event.active == 'fullName') {
      this.disasterVictimRequestModel.sortModels.push({
        sortName: 'firstName',
        sortDirection: sortDirection
      } as SearchSortModel);
      this.disasterVictimRequestModel.sortModels.push({
        sortName: 'lastName',
        sortDirection: sortDirection
      } as SearchSortModel);
    } else if ($event.active == 'cityCountryName') {
      this.disasterVictimRequestModel.sortModels.push({
        sortName: 'cityName',
        sortDirection: sortDirection
      } as SearchSortModel);
      this.disasterVictimRequestModel.sortModels.push({
        sortName: 'countryName',
        sortDirection: sortDirection
      } as SearchSortModel);
    } else {
      this.disasterVictimRequestModel.sortModels.push({
        sortName: $event.active,
        sortDirection: sortDirection
      } as SearchSortModel);
    }
    this.onSearch();
  }

  edit(id: string) {
    this.navigationService.navigate('disaster-victim/edit/' + id);
  }

  delete(userId: string)
  {
    this.dialog
      .open(ConfirmationDialogComponent, {
        width: '300px',
        data: {
          title: 'Uyar??',
          message: 'Kayd?? silmek istedi??inize emin misiniz?'
        }
      })
      .afterClosed()
      .pipe(
        filter((result) => {
          return result;
        }),
        switchMap(() => this.disasterVictimService.delete(userId)),
        tap(() => this.snackbar.show('Success', 'Kayit silindi')),
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

  override onCheckboxChange(isChecked: boolean) {
    super.onCheckboxChange(isChecked);
    this.onSearch();
  }
}
