import { Routes } from '@angular/router';
import { AuthenticationGuard } from '@appModule/guards/authentication.guard/authentication.guard';
const disasterVictimListRoutes: Routes = [
  {
    path: 'disaster-victim-list',
    loadComponent: () =>
      import(
        '@disasterVictimListsModule/components/disaster-victim-list/disaster-victim-list.component'
      ),
    canActivate: [AuthenticationGuard]
  },
  {
    path: 'disaster-victim-list/add',
    loadComponent: () =>
      import(
        '@disasterVictimListsModule/components/add-disaster-victim/add-disaster-victim.component'
      ),
    canActivate: [AuthenticationGuard]
  },
  {
    path: 'disaster-victim/edit/:id',
    loadComponent: () =>
      import(
        '@disasterVictimListsModule/components/add-disaster-victim/add-disaster-victim.component'
      ),
    canActivate: [AuthenticationGuard]
  }
];
export default disasterVictimListRoutes;
