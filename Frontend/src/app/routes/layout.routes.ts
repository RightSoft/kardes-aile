import { Routes } from '@angular/router';
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
      },
      ...voluntarilyListRoutes,
    ],
  },
];
export default layoutRoutes;
