import { Routes } from '@angular/router';
import { AuthenticationGuard } from '../guards/authentication.guard/authentication.guard';
const moderatorRoutes: Routes = [
  {
    path: 'moderator',
    loadComponent: () => import('@moderatorModule/components/moderator-list/moderator-list.component'),
    canActivate: [AuthenticationGuard]
  },
  {
    path: 'moderator/new',
    loadComponent: () => import('@moderatorModule/components/moderator-create/moderator-create.component'),
    canActivate: [AuthenticationGuard]
  },
  {
    path: 'moderator/:id',
    loadComponent: () => import('@moderatorModule/components/moderator-update/moderator-update.component'),
    canActivate: [AuthenticationGuard]
  }
];
export default moderatorRoutes;
