import { Routes } from '@angular/router';

const publicRoutes: Routes = [
  {
    path: 'public',
    loadComponent: () =>
      import(
        '@landingModule/public-facing/components/public-facing.component'
        )
  },
  {
    path: 'thanks',
    loadComponent: () =>
      import(
        '@landingModule/public-facing/components/supporter-persisted.component'
        )
  }
];
export default publicRoutes;
