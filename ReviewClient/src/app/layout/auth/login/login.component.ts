import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ApiService } from 'src/app/shared/services/api.service';
import { AuthService } from 'src/app/shared/services/auth.service';
import { RouterService } from 'src/app/shared/services/router.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  register:any='/auth/register'
  form:any;
  submitted:boolean=false;
  constructor(
    private fb:FormBuilder,
    private _apiService:ApiService,
    private _authService:AuthService,
    private _router:Router,
    private _toastr:ToastrService
  ) { }

  ngOnInit(): void {
    
    this.createForm();

  }

  createForm(){
    this.form = this.fb.group({
      email:['',Validators.required],
      password:['',Validators.required]
    });
  }

  login(){
    this.submitted=true
    if(!this.form.valid) return;
    this._apiService.Post('user','login',this.form.value).subscribe((res:any)=>{
      if(res.success){
        this._authService.saveUser(res.data);
        this._toastr.success('Welcome Back');
        this.form.reset();
        this.submitted = false;
        this._router.navigate(['/dashboard']);
      }
      else{
        this._toastr.error(res.message)
      }
    },err=>{
      alert('Connection Problem');
    })
  }


}
