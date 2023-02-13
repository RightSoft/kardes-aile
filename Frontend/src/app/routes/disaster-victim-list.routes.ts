import { Routes } from '@angular/router';
const disasterVictimListRoutes: Routes = [
  {
    path: 'disaster-victim-list',
    loadComponent: () =>
      import(
        '@disasterVictimListsModule/components/disaster-victim-list/disaster-victim-list.component'
      )
  },
  {
    path: 'disaster-victim-list/add',
    loadComponent: () =>
      import(
        '@disasterVictimListsModule/components/add-disaster-victim/add-disaster-victim.component'
      )
  },
  {
    path: 'disaster-victim/edit/:id',
    loadComponent: () =>
      import(
        '@disasterVictimListsModule/components/add-disaster-victim/add-disaster-victim.component'
      )
  }
];
export default disasterVictimListRoutes;
