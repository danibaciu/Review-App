import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ApiService } from 'src/app/shared/services/api.service';
import { AuthService } from 'src/app/shared/services/auth.service';
declare var $:any;
@Component({
  selector: 'app-article-types',
  templateUrl: './article-types.component.html',
  styleUrls: ['./article-types.component.scss']
})
export class ArticleTypesComponent implements OnInit {
  page:number=1;
  types:any[]=[];
  user:any={};
  
  form:any;
  submitted:boolean=false;
  constructor(
    private _authService:AuthService,
    private _toastr:ToastrService,
    private _apiService:ApiService,
    private fb:FormBuilder

  ) { 
    this.user=_authService.getUser();
  }

  ngOnInit(): void {
    this.form = this.fb.group({
      tag:['',Validators.required]
    })
    this.getTypes();
  }

  getTypes(){
    this._apiService.Get('articleType','','').subscribe((res:any)=>{
      if(res.success){
        this.types=[];
        this.types=res.data;
        console.log(this.types)
      }
      else{
        this._toastr.error(res.message);
      }
    },err =>{
      this._toastr.error('Connection Problem');
    })
  }

  insertNewTag(){
    this.submitted=true;
    if(!this.form.valid) return;
    this._apiService.Post('articleType','',this.form.value).subscribe((res:any)=>{
      if(res.success){
        this._toastr.success(res.message);
        this.form.reset();
        this.submitted=false;
        this.getTypes();
        $("#insertDialog").modal('hide')
      }
      else{
        this._toastr.error(res.message);
      }
    },err =>{
      this._toastr.error('Connection problem');
    })
  }

  deleteType(type:any){
    this._apiService.Delete('articleType',`${type.id}`,'').subscribe((res:any)=>{
      if(res.success){
        this._toastr.success(res.message);
        this.getTypes();
      }
      else{
        this._toastr.error(res.message);
      }
    },err=>{
      this._toastr.error('Connection Problem');
    })
  }


}


