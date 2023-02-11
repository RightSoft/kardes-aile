import { Routes } from '@angular/router';
import voluntarilyRoutes from './voluntarily.routes';
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
      ...voluntarilyRoutes,
    ],
  },
];
export default layoutRoutes;
