import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatDialog, MatDialogModule} from '@angular/material/dialog';
import BaseListComponent from '@appModule/base-classes/base-list-component.abstract.class';
import {ListToolBoxComponent} from '@sharedComponents/list-tool-box/list-tool-box.component';
import {NavigationService} from "@appModule/services/navigation.service";
import {SnackbarService} from "@appModule/services/snackbar.service";
import {MatTableDataSource, MatTableModule} from "@angular/material/table";
import {PagedResultModel} from "@appModule/models/shared/paged-result.model";
import {MatPaginator, MatPaginatorModule, PageEvent} from "@angular/material/paginator";
import {MatSortModule} from "@angular/material/sort";
import {
  ConfirmationDialogComponent
} from "@sharedComponents/confirmation-dialog/components/confirmation-dialog.component";
import {catchError, filter, of, switchMap, tap} from "rxjs";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {FlexModule} from "@angular/flex-layout";
import {SvgIconModule} from "@appModule/modules/svg-icon.module";
import {MatTooltipModule} from "@angular/material/tooltip";
import {MatchListsService} from "@matchListsModule/business/match-lists.service";
import {MatchSearchResultModel} from "@appModule/models/match/match-search-result.model";
import {SearchMatchModel} from "@appModule/models/match/search-match.model";
import AddNewMatchComponent from "@matchListsModule/components/add-new-match/add-new-match.component";

@Component({
  selector: 'app-match-lists',
  standalone: true,
  imports: [
    CommonModule,
    ListToolBoxComponent,
    MatTableModule,
    MatPaginatorModule,
    MatButtonModule,
    MatIconModule,
    MatSortModule,
    MatDialogModule,
    FlexModule,
    SvgIconModule,
    MatTooltipModule],
  templateUrl: './match-lists.component.html',
  styleUrls: ['./match-lists.component.scss']
})
export default class MatchListsComponent extends BaseListComponent
  implements AfterViewInit {
  displayedColumns: string[] = [
    'voluntarilyFullName',
    'victimFullName',
    'createdAt',
    'status',
    'actions'
  ];
  dataSource: MatTableDataSource<MatchSearchResultModel> =
    new MatTableDataSource<MatchSearchResultModel>();
  pagedSupporterData: PagedResultModel<MatchSearchResultModel> =
    new PagedResultModel<MatchSearchResultModel>();
  searchMatchData: SearchMatchModel = new SearchMatchModel(1, 10);
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    private matchListsService: MatchListsService,
    private navigationService: NavigationService,
    private dialog: MatDialog,
    private snackbar: SnackbarService) {
    super('Eşleşme Listesi');
    this.onSearch();
  }

  addNewMatch() {
    this.dialog.open(AddNewMatchComponent);
  }

  ngAfterViewInit() {
    this.dataSource = new MatTableDataSource<MatchSearchResultModel>([]);
    this.dataSource.paginator = this.paginator;
  }

  changePaging($event: PageEvent) {
    this.searchMatchData.page = $event.pageIndex;
    this.onSearch();
  }

  override onSearch(keyword?: string) {
    if (keyword !== undefined) this.searchMatchData.filter = keyword;
    this.searchMatchData.includePassives = this.isChecked;

    this.matchListsService
      .search(this.searchMatchData)
      .subscribe((result) => {
        this.dataSource = new MatTableDataSource<MatchSearchResultModel>(
          result.list
        );
        this.pagedSupporterData = result;
      });
  }

  onEdit(id: string) {
    this.dialog.open(AddNewMatchComponent, {
      data: {id}
    });
  }

  onDelete(matchId: string) {
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
        filter((result) => {
          return result;
        }),
        switchMap(() => this.matchListsService.delete(matchId)),
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

  getMatchStatusLabel(status: boolean) {
    return status ? 'Aktif' : 'İptal';
  }

  override onCheckboxChange(isChecked: boolean) {
    super.onCheckboxChange(isChecked);
    this.onSearch();
  }
}
