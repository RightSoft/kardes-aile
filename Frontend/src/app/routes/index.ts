import { Routes } from '@angular/router';
import authRoutes from './auth.routes';
import layoutRoutes from './layout.routes';
const projectRoutes: Routes = [...authRoutes, ...layoutRoutes];

export default projectRoutes;
