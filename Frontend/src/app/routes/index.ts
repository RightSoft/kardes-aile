import { Routes } from '@angular/router';
import authRoutes from './auth.routes';
import layoutRoutes from './layout.routes';
import publicRoutes from './public.routes';

const projectRoutes: Routes = [...authRoutes, ...layoutRoutes, ...publicRoutes];

export default projectRoutes;
