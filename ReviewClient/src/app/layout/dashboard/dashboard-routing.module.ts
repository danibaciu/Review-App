import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ArticleDetailComponent } from './article-detail/article-detail.component';
import { ArticleTypesComponent } from './article-types/article-types.component';
import { ArticlesComponent } from './articles/articles.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { UpdateProfileComponent } from './update-profile/update-profile.component';

const routes: Routes = [
  {
    path:'',component:DashboardComponent
  },
  {
    path:'articles',component:ArticlesComponent
  },
  {path:'update-profile',component:UpdateProfileComponent},
  {path:'article-detail/:id',component:ArticleDetailComponent},
  {path:'article-types',component:ArticleTypesComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
