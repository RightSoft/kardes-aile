import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ListToolBoxComponent} from '@sharedComponents/list-tool-box/list-tool-box.component';
import BaseListComponent from '@appModule/base-classes/base-list-component.abstract.class';
import {MatTableDataSource, MatTableModule} from "@angular/material/table";
import {MatPaginator, MatPaginatorModule, PageEvent} from "@angular/material/paginator";
import {PagedResultModel} from "@appModule/models/paged-result.model";
import {SupporterSearchResultModel} from "@appModule/models/supporter-search-result.model";
import {SearchSupporterModel} from "@appModule/models/search-supporter.model";
import {VoluntarilyService} from "@voluntarilyListsModule/business/voluntarily.service";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import { MatSortModule, Sort } from "@angular/material/sort";
import {SortDirection} from "@appModule/models/shared/sort-direction.enum";
import {SearchSortModel} from "@appModule/models/search-sort.model";

@Component({
  selector: 'app-voluntarily-list',
  standalone: true,
  imports: [CommonModule, ListToolBoxComponent, MatTableModule, MatPaginatorModule, MatButtonModule, MatIconModule, MatSortModule],
  templateUrl: './voluntarily-list.component.html',
  styleUrls: ['./voluntarily-list.component.scss']
})
export default class VoluntarilyListComponent extends BaseListComponent implements AfterViewInit{

  displayedColumns: string[] = ['fullName', 'cityCountyName', 'matchingStatus', 'createdAt', 'actions'];
  dataSource: MatTableDataSource<SupporterSearchResultModel> = new MatTableDataSource<SupporterSearchResultModel>();
  pagedSupporterData: PagedResultModel<SupporterSearchResultModel> = new PagedResultModel<SupporterSearchResultModel>();
  searchSupporterData: SearchSupporterModel = new SearchSupporterModel(1, 10);

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private voluntarilyService: VoluntarilyService) {
    super('Gönüllü Listesi');
    this.onSearch();
  }

  ngAfterViewInit() {
    this.dataSource = new MatTableDataSource<SupporterSearchResultModel>([]);
    this.dataSource.paginator = this.paginator;
  }

  changePaging($event: PageEvent){
    this.searchSupporterData.page = $event.pageIndex;
    this.onSearch();
  }

  onSorting($event: Sort){
    const sortDirection = $event.direction == 'desc' ? SortDirection.Descending : SortDirection.Ascending;
    this.searchSupporterData.sortModels = [];
    if($event.active == 'fullName'){
      this.searchSupporterData.sortModels.push({ sortName: 'firstName', sortDirection: sortDirection } as SearchSortModel);
      this.searchSupporterData.sortModels.push({ sortName: 'lastName', sortDirection: sortDirection } as SearchSortModel);
    }
    else if($event.active == 'cityCountryName') {
      this.searchSupporterData.sortModels.push({ sortName: 'cityName', sortDirection: sortDirection } as SearchSortModel);
      this.searchSupporterData.sortModels.push({ sortName: 'countryName', sortDirection: sortDirection } as SearchSortModel);
    }
    else {
      this.searchSupporterData.sortModels.push({ sortName: $event.active, sortDirection: sortDirection } as SearchSortModel);
    }
    this.onSearch();
  }
  override onSearch(keyword?: string) {
    if(keyword)
      this.searchSupporterData.keyword = keyword;

    this.voluntarilyService.search(this.searchSupporterData).subscribe((result) => {
      this.dataSource = new MatTableDataSource<SupporterSearchResultModel>(result.list);
      this.pagedSupporterData = result;
    });
  }
}
