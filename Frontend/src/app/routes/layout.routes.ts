import { Routes } from '@angular/router';
import voluntarilyListRoutes from './voluntarily-list.routes';
import moderatorRoutes from './moderator.routes';
import disasterVictimListRoutes from './disaster-victim-list.routes';
import matchListRoutes from "@appModule/routes/match-list.routes";
const layoutRoutes: Routes = [
  {
    path: 'private',
    loadComponent: () =>
      import('@layoutModule/components/layout/layout.component'),
    children: [
      ...voluntarilyListRoutes,
      ...matchListRoutes,
      ...moderatorRoutes,
      ...disasterVictimListRoutes
    ]
  }
];
export default layoutRoutes;
