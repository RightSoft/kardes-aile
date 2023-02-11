import { Routes } from '@angular/router';
const moderatorRoutes: Routes = [
  {
    path: 'moderator',
    loadComponent: () =>
      import('@moderatorModule/components/moderator-list/moderator-list.component'),
  },
];
export default moderatorRoutes;
