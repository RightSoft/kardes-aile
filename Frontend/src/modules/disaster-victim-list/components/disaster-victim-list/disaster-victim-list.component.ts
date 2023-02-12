import { DisasterVictimService } from './../../business/disaster-victim.service';
import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListToolBoxComponent } from '@sharedComponents/list-tool-box/list-tool-box.component';
import BaseListComponent from '@appModule/base-classes/base-list-component.abstract.class';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-disaster-victim-list',
  standalone: true,
  imports: [CommonModule, ListToolBoxComponent],
  templateUrl: './disaster-victim-list.component.html',
  styleUrls: ['./disaster-victim-list.component.scss']
})
export default class DisasterVictimListComponent
  extends BaseListComponent
  implements OnInit
{
  private disasterVictimService = inject(DisasterVictimService);
  constructor() {
    super('Afetzede Listesi');
  }
  ngOnInit(): void {
    this.searchDisasterVictim();
  }
  async searchDisasterVictim() {
    const request$ = this.disasterVictimService.searchDisasterVictim(
      1,
      10,
      this.isChecked
    );
    const response = await lastValueFrom(request$);
    console.log(response);
  }
}
