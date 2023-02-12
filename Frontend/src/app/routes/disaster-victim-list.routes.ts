import { Routes } from '@angular/router';
const disasterVictimListRoutes: Routes = [
  {
    path: 'disaster-victim-list',
    loadComponent: () =>
      import(
        '@disasterVictimListsModule/components/disaster-victim-list/disaster-victim-list.component'
      )
  }
];
export default disasterVictimListRoutes;
