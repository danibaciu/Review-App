import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ApiService } from 'src/app/shared/services/api.service';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {
  roles:any[]=[];
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
    this.getRoles();
    this.createForm();

  }

  createForm(){
    this.form = this.fb.group({
      email:['',Validators.required],
      displayName:['',Validators.required],
      password:['',Validators.required],
      gdpr_acceptance:[false],
      darkMode:[false],
      age:[0,],
      roleId:['',Validators.required],
    });
  }

  getRoles(){
    this._apiService.Get('roles','getRoles','').subscribe((res:any)=>{
      if(res.success){
        let index = res.data.findIndex((x:any)=>{
          return x.roleName.toLowerCase() === "admin";
        });
        res.data.splice(index,1);
        this.roles=[];
        this.roles=res.data;
      }
    })
  }

  register(){
    this.submitted=true
    if(!this.form.valid) return;
    this._apiService.Post('user','register',this.form.value).subscribe((res:any)=>{
      if(res.success){
        this._authService.saveUser(res.data);
        this._toastr.success('Welcome!');
        this.form.reset();
        this.submitted = false;
        this._router.navigate(['/dashboard']);
      }
      else{
        this._toastr.error(res.message)
      }
    },err=>{
      this._toastr.error('Connection Problem');
    })
  }

}
