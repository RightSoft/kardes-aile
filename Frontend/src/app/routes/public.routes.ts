import { Routes } from '@angular/router';
const publicRoutes: Routes = [
    {
        path: 'public',
        loadComponent: () =>
            import(
                '@landingModule/public-facing/components/public-facing.component'
            ),
        canActivate: []
    },
];
export default publicRoutes;
