import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import {NgxPaginationModule} from 'ngx-pagination'; // <-- import the module

import { FormsModule,ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [HeaderComponent, FooterComponent, SidebarComponent],
  imports: [
    CommonModule,FormsModule,NgxPaginationModule
  ],
  exports:[
    HeaderComponent, FooterComponent, SidebarComponent,FormsModule,CommonModule,ReactiveFormsModule,NgxPaginationModule
  ]
})
export class SharedModule { }
