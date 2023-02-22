import { Routes } from '@angular/router';
import { AuthenticationGuard } from '@appModule/guards/authentication.guard/authentication.guard';
import matchListRoutes from "@appModule/routes/match-list.routes";
import auditRoutes from './audits.routes';
import disasterVictimListRoutes from './disaster-victim-list.routes';
import moderatorRoutes from './moderator.routes';
import voluntarilyListRoutes from './voluntarily-list.routes';
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
      ...matchListRoutes,
      ...moderatorRoutes,
      ...disasterVictimListRoutes,
      ...auditRoutes
    ]
  }
];
export default layoutRoutes;
