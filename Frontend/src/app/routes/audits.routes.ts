import { Routes } from '@angular/router';
import { AuthenticationGuard } from '../guards/authentication.guard/authentication.guard';
const auditRoutes: Routes = [
  {
    path: 'audits',
    loadComponent: () => import('@auditsModule/components/list/audits-list.component'),
    canActivate: [AuthenticationGuard]
  }
];
export default auditRoutes;
