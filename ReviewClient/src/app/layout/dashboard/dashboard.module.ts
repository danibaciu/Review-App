import { NgModule } from '@angular/core';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { SharedModule } from 'src/app/shared/module/shared.module';
import { DashboardLayoutComponent } from './dashboard-layout/dashboard-layout.component';
import { UpdateProfileComponent } from './update-profile/update-profile.component';
import { ArticlesComponent } from './articles/articles.component';
import { ArticleDetailComponent } from './article-detail/article-detail.component';
import { ArticleTypesComponent } from './article-types/article-types.component';


@NgModule({
  declarations: [ DashboardComponent,DashboardLayoutComponent, UpdateProfileComponent, ArticlesComponent, ArticleDetailComponent, ArticleTypesComponent],
  imports: [
    SharedModule,
    
    DashboardRoutingModule
  ]
})
export class DashboardModule { }
