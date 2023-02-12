import { Routes } from '@angular/router';
import voluntarilyListRoutes from './voluntarily-list.routes';
import moderatorRoutes from './moderator.routes';
import { AuthenticationGuard } from '@appModule/guards/authentication.guard/authentication.guard';
import disasterVictimListRoutes from './disaster-victim-list.routes';
const layoutRoutes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('@layoutModule/components/layout/layout.component'),
    children: [
      {
        path: '',
        loadComponent: () =>
          import(
            '@matchListsModule/components/match-lists/match-lists.component'
          ),
        canActivate: [AuthenticationGuard]
      },
      ...voluntarilyListRoutes,
      ...moderatorRoutes,
      ...disasterVictimListRoutes
    ]
  }
];
export default layoutRoutes;
