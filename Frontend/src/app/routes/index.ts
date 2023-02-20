import { Routes } from '@angular/router';
import authRoutes from './auth.routes';
import layoutRoutes from './layout.routes';
import publicRoutes from './public.routes';

const projectRoutes: Routes = [...publicRoutes, ...authRoutes, ...layoutRoutes];

export default projectRoutes;
