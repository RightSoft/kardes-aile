import { Routes } from '@angular/router';
const voluntarilyListRoutes: Routes = [
  {
    path: 'voluntarily',
    loadComponent: () =>
      import(
        '@voluntarilyListsModule/components/voluntarily-list/voluntarily-list.component'
      )
  },
  {
    path: 'voluntarily/add',
    loadComponent: () =>
      import(
        '@voluntarilyListsModule/components/add-voluntarily/add-voluntarily.component'
      )
  }
];
export default voluntarilyListRoutes;
