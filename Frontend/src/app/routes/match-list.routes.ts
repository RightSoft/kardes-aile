import {Routes} from '@angular/router';
import {AuthenticationGuard} from "@appModule/guards/authentication.guard/authentication.guard";

const matchListRoutes: Routes = [
  {
    path: 'match',
    loadComponent: () => import('@matchListsModule/components/match-lists/match-lists.component'),
    canActivate: [AuthenticationGuard]
  },
  {
    path: 'match/add',
    loadComponent: () => import('@matchListsModule/components/add-new-match/add-new-match.component'),
    canActivate: [AuthenticationGuard]
  },
  {
    path: 'match/:id',
    loadComponent: () => import('@matchListsModule/components/add-new-match/add-new-match.component'),
    canActivate: [AuthenticationGuard]
  }
];
export default matchListRoutes;
