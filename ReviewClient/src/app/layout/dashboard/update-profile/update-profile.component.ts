import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ApiService } from 'src/app/shared/services/api.service';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-update-profile',
  templateUrl: './update-profile.component.html',
  styleUrls: ['./update-profile.component.scss']
})
export class UpdateProfileComponent implements OnInit {
  profile:any;
  profileSubmitted:boolean=false;
  
  user:any;
  userSubmitted:boolean=false;
  
  preference:any;
  preferenceSubmitted:boolean=false;

  currentUser:any;
  constructor(
    private fb:FormBuilder,
    private _authService:AuthService,
    private _router:Router,
    private _toastr:ToastrService,
    private _apiService:ApiService
  ) {
    if(this._authService.islogin()){
      this.currentUser = this._authService.getUser()
    }
   }

  ngOnInit(): void {
    this.createForm();
  }

  createForm(){
    this.profile = this.fb.group({
      userId:[this.currentUser.userId || 0],
      displayName:[this.currentUser.profile.displayName || '',Validators.required],
      age:[this.currentUser.profile.age || 0,Validators.required]
    })
    this.user = this.fb.group({
      userId:[this.currentUser.userId || 0],
      email:[this.currentUser.email || '',Validators.required],
      password:['',Validators.required]
    })
    this.preference = this.fb.group({
      userId:[this.currentUser.userId || 0],
      darkMode:[this.currentUser.preference.darkMode || false,Validators.required],
      gdpr_acceptance:[this.currentUser.preference.gdpr_acceptance || false,Validators.required]
    })
  }

  updateUser(){
    this.userSubmitted=true;
    if(!this.user.valid)return;
    this._apiService.Put('user','updateUser',this.user.value).subscribe((res:any)=>{
      this.userSubmitted=false
      if(res.success){
        this._toastr.success(res.message);
        this.user.reset();
        delete res.data.password;
        this.currentUser.user = res.data;
        this._authService.saveUser(this.currentUser);
        this.createForm();
      }
      else{
        
        this._toastr.error(res.message);
      }
    },err=>{
      this.userSubmitted=false
      this._toastr.error('Connection problem');
    })
  }

  updatePreference(){
    this.preferenceSubmitted=true;
    if(!this.preference.valid)return;
    console.log(this.preference.value)
    this._apiService.Put('preference','updatePreference',this.preference.value).subscribe((res:any)=>{
      this.preferenceSubmitted=false
      if(res.success){
        this._toastr.success(res.message);
        this.preference.reset();
        
        this.currentUser.preference = res.data;
        this._authService.saveUser(this.currentUser);
        this.createForm();
      }
      else{
        
        this._toastr.error(res.message);
      }
    },err=>{
      this.preferenceSubmitted=false
      this._toastr.error('Connection problem');
    })
  }

  updateProfile(){
    this.profileSubmitted=true;
    if(!this.profile.valid)return;
    this._apiService.Put('profile','updateProfile',this.profile.value).subscribe((res:any)=>{
      this.profileSubmitted=false
      if(res.success){
        this._toastr.success(res.message);
        this.profile.reset();
        this.currentUser.profile = res.data;
        this._authService.saveUser(this.currentUser);
        this.createForm();
      }
      else{
        
        this._toastr.error(res.message);
      }
    },err=>{
      this.profileSubmitted=false
      this._toastr.error('Connection problem');
    })
  }

}
