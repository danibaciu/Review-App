import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
declare const $: any;
@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {

  user:any;

  constructor(private _Router: Router,private _authService:AuthService) {
    this.getUser();
   }

  ngOnInit(): void {
  }
  getUser(){
    if(this._authService.islogin()){
      this.user = this._authService.getUser();
      console.log(this.user)
    }
  }

  toggle(): void {
    $("#kt_body").toggle('.aside-minimize');
  }

  navigate(link: string): void {
    this._Router.navigateByUrl('/' + link);
  }
 
}
