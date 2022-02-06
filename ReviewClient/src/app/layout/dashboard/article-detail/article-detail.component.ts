import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ApiService } from 'src/app/shared/services/api.service';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-article-detail',
  templateUrl: './article-detail.component.html',
  styleUrls: ['./article-detail.component.scss']
})
export class ArticleDetailComponent implements OnInit {
  article:any={};
  user:any={};
  form:any;
  comments:any[]=[];
  submitted:boolean=false;
  page:number=1;


  constructor(
    private _route:ActivatedRoute,
    private _authService:AuthService,
    private _apiService:ApiService,
    private _toastr:ToastrService,
    private fb:FormBuilder

  ) { 
    if(_authService.islogin()){
      this.user= _authService.getUser();
    }
  }

  ngOnInit(): void {
    this.getComments();
    this.getArticle();
    this.createForm();
  }

  createForm(){
    this.form=this.fb.group({
      id:[0],
      text:['',Validators.required],
      userId:[this.user.userId],
      articleId:[this._route.snapshot.paramMap.get('id')]
    })
  }

  getComments(){
    this._apiService.Get('comment',`article/${this._route.snapshot.paramMap.get('id')}`,'').subscribe((res:any)=>{
      if(res.success){
        this.comments =[];
        this.comments=res.data;
        console.log(this.comments)
      }
      else{
        this._toastr.error(res.message);
      }
    },err=>{
      this._toastr.error('Connection Problem');
    })
  }

  insertComment(){
    this.submitted=true;
    if(!this.form.valid)return;
    if(this.form.controls['text'].value.length > 100) {
      this._toastr.info('Comment must not be greatar than 100 words');
      return;
    }
    this._apiService.Post('comment','insertComment',this.form.value).subscribe((res:any)=>{
      this.submitted=false;
      if(res.success){
        this._toastr.success(res.message);
        this.form.reset();
        this.getComments();
      }
      else{
        this._toastr.error(res.message);
      }
    },err=>{
      this._toastr.error('Connection Problem');
    })
  }

  getArticle(){
    const articleId = this._route.snapshot.paramMap.get('id');
    this._apiService.Get('article',`${articleId}`).subscribe((res:any)=>{
      if(res.success){
        this.article=res.data;
        console.log(res.data);
      }
      else{
        this._toastr.error(res.message);
      }
    },err=>{
      this._toastr.error('Connection Problem');
    })
  }

}
