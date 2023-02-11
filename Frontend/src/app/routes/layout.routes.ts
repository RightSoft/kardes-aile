import { Routes } from '@angular/router';
import voluntarilyListRoutes from './voluntarily-list.routes';
import moderatorRoutes from './moderator.routes';
import { AuthenticationGuard } from "@appModule/guards/authentication.guard/authentication.guard";
const layoutRoutes: Routes = [
  {
    path: '',
    loadComponent: () => import('@layoutModule/components/layout/layout.component'),
    children: [
      {
        path: '',
        loadComponent: () => import('@matchListsModule/components/match-lists/match-lists.component'),
        canActivate: [AuthenticationGuard]
      },
      ...voluntarilyListRoutes,
      ...moderatorRoutes
    ],
  },
];
export default layoutRoutes;
