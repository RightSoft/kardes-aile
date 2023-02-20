import { Routes } from '@angular/router';
import { AuthenticationGuard } from '@appModule/guards/authentication.guard/authentication.guard';
const voluntarilyListRoutes: Routes = [
  {
    path: 'voluntarily',
    loadComponent: () =>
      import(
        '@voluntarilyListsModule/components/voluntarily-list/voluntarily-list.component'
      ),
    canActivate: [AuthenticationGuard]
  },
  {
    path: 'voluntarily/add',
    loadComponent: () =>
      import(
        '@voluntarilyListsModule/components/add-voluntarily/add-voluntarily.component'
      ),
    canActivate: [AuthenticationGuard]
  },
  {
    path: 'voluntarily/:id',
    loadComponent: () =>
      import(
        '@voluntarilyListsModule/components/add-voluntarily/add-voluntarily.component'
        ),
    canActivate: [AuthenticationGuard]
  }
];
export default voluntarilyListRoutes;
