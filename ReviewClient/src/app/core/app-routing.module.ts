import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardLayoutComponent } from '../layout/dashboard/dashboard-layout/dashboard-layout.component';
import { AdminGuard } from '../shared/guards/admin_guard.service';
import { LoginGuard } from '../shared/guards/login_guard.service';

const routes: Routes = [
  {
    path:'',
    component:DashboardLayoutComponent,
    canActivate:[AdminGuard],
    loadChildren:()=>import('../layout/dashboard/dashboard.module').then(m=>m.DashboardModule)
  },
  {
    path:'auth',
    canActivate:[LoginGuard],
    loadChildren:()=>import('../layout/auth/auth.module').then(m=>m.AuthModule)
  },
  {
    path:'**',
    redirectTo:'',
    pathMatch:'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
