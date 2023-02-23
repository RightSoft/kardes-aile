import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSelectModule } from '@angular/material/select';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { map, merge, startWith, switchMap } from 'rxjs';
import BaseListComponent from '../../../../app/base-classes/base-list-component.abstract.class';
import { SortDirection } from '../../../../app/models/shared/sort-direction.enum';
import { SvgIconModule } from '../../../../app/modules/svg-icon.module';
import { AuditsService } from '../../business/audits.service';
import { AuditTypesEnum } from '../../enums/audit-types.enum';
import { AuditSearchResultModel } from '../../models/audit-search-result.model';
import { AuditSearchModel } from '../../models/audit-search.model';

@Component({
  selector: 'app-audits-list',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    SvgIconModule,
    FlexLayoutModule
  ],
  templateUrl: './audits-list.component.html',
  styleUrls: ['./audits-list.component.scss']
})
export default class AuditsListComponent extends BaseListComponent implements AfterViewInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  displayedColumns = [
    'type',
    "action",
    "createdBy",
    "createdAt"
  ];

  pageSizeOptions = [10, 20, 50, 100];
  pageSize = 20;

  dataSource: MatTableDataSource<AuditSearchResultModel> = new MatTableDataSource([]);
  length = 0;

  form: FormGroup;

  auditTypesEnum = AuditTypesEnum;

  get auditTypes() {
    return Object.keys(AuditTypesEnum)
      .filter((key) => typeof AuditTypesEnum[parseInt(key)] === 'string');
  }

  getEnumKeyAsNumber(key: string) {
    return parseInt(key);
  }

  constructor(
    private auditsService: AuditsService,
    private formBuilder: FormBuilder
  ) {
    super('İşlem Kayıtları');

    this.form = this.formBuilder.group({
      'filter': this.formBuilder.control(''),
      'type': this.formBuilder.control(null),
      'start': this.formBuilder.control(null),
      'end': this.formBuilder.control(null)
    });
  }

  ngAfterViewInit() {

    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;

    merge(this.dataSource.paginator.page, this.dataSource.sort.sortChange)
      .pipe(
        startWith({}),
        switchMap(() => this.onSearch()),
        map(result => {
          this.length = result.totalCount;
          return result.list;
        })
      ).subscribe(audits => {
        this.dataSource = new MatTableDataSource<AuditSearchResultModel>(audits);
      });
  }

  override onSearch() {

    let model = new AuditSearchModel();

    model.filter = this.form.get('filter').value?.trim();
    model.type = this.form.get('type').value
      ? parseInt(this.form.get('type').value)
      : null;
    model.start = this.form.get('start').value;
    model.end = this.form.get('end').value;
    model.pageSize = this.paginator.pageSize;
    model.page = this.paginator.pageIndex + 1;

    model.sortModels = [{
      sortName: this.sort.active ?? 'createdAt',
      sortDirection: this.sort.direction === 'desc'
        ? SortDirection.Descending
        : SortDirection.Ascending
    }];

    return this.auditsService.search(model);
  }

  onSubmit() {
    this.onSearch()
      .subscribe(result => {
        this.length = result.totalCount;
        this.dataSource = new MatTableDataSource<AuditSearchResultModel>(result.list);
      });
  }
}

