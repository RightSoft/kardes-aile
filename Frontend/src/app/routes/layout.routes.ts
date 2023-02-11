import { Routes } from '@angular/router';
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
    ],
  },
];
export default layoutRoutes;
