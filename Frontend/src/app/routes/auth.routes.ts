import { Routes } from '@angular/router';
const authRoutes: Routes = [
  {
    path: 'auth',
    loadComponent: () => import('@authModule/components/login/login.component'),
  },
];
export default authRoutes;
