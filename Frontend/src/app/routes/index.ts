import { Routes } from '@angular/router';
import authRoutes from './auth.routes';
const projectRoutes: Routes = [
  ...authRoutes,
  {
    path: '',
    loadComponent: () =>
      import('@dashboardModule/components/dashboard/dashboard.component'),
  },
];

export default projectRoutes;
