import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
declare const $: any;
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  user:any;
  constructor(private _authService:AuthService) {
    this.user = _authService.getUser();
   }

  ngOnInit(): void {
  }

  OpenUserProfileSideBarModal(){
    if($("#kt_quick_user").hasClass("offcanvas-on")){
      $("#kt_quick_user").removeClass('offcanvas-on')
    }else{
      $("#kt_quick_user").addClass('offcanvas-on')
    }
  }

  logout(){
    this._authService.logout();
  }

  setProfileImage(){
    let styles={
      'background-image':this.user.profileImage == null? `url(assets/media/users/default.jpg)` : `url(${this.user.profileImage})`
    }
    return styles;
  }

}
