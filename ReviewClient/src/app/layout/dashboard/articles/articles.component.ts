import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ApiService } from 'src/app/shared/services/api.service';
import { AuthService } from 'src/app/shared/services/auth.service';
declare var $:any;
@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.scss']
})
export class ArticlesComponent implements OnInit {
  articles:any[]=[];
  page:number=1;
  selectedArticle:any={};

  user:any;

  articleTags:any[]=[];
  

  form:any;
  isEdit:boolean=false;
  submitted:boolean=false;
  constructor(
    private _authService:AuthService,
    private _apiService:ApiService,
    private _toastr:ToastrService,
    private _router:Router,
    private fb:FormBuilder
  ) {
    if(_authService.islogin()) this.user= _authService.getUser();
    console.log(this.user)
   }

  ngOnInit(): void {
    this.createForm();
    this.getArticleCats();
    this.getArticles();
  }

  getArticleCats(){
    this._apiService.Get('articleType','','').subscribe((res:any)=>{
      if(res.success){
        this.articleTags=[];
        this.articleTags=res.data;
        console.log(this.articleTags)
      }
      else {
        this._toastr.error(res.message);
      }
    },err=>{
      this._toastr.error('Connection Problem');
    })
  }

  createForm(){
    this.form = this.fb.group({
      id:[0],
      title:['',Validators.required],
      content:['',Validators.required],
      userId:[this.user.userId],
      articleCatId:['',Validators.required]
    })
  }
  editArticle(article:any){
    this.isEdit=true;
    this.form.controls['id'].setValue(article.id);
    this.form.controls['articleCatId'].setValue(article.articleTagId);
    this.form.controls['title'].setValue(article.title);
    this.form.controls['content'].setValue(article.content);
    this.form.controls['userId'].setValue(article.userId);

  }
  
  addArticle(){
    this.isEdit=false;
    this.form.reset();
    this.createForm();
  }
  getArticleByTag(event:any){
    if(event.target.value == 0){
      this.getArticles();
    }
    else{
      this._apiService.Get('article',`tag/${event.target.value}`,'').subscribe((res:any)=>{
        if(res.success){
          this.articles = [];
          this.articles = res.data;
          console.log(this.articles)
        }
        else{
          this._toastr.error(res.message)
        }
      },err=>{
        this._toastr.error('Connection Problem');
      })
    }
   
  }

  getArticles(){
    this._apiService.Get('article','','').subscribe((res:any)=>{
      if(res.success){
        this.articles = [];
        this.articles = res.data;
        console.log(this.articles)
      }
      else{
        this._toastr.error(res.message)
      }
    },err=>{
      this._toastr.error('Connection Problem');
    })
  }

  insert(){
    this.submitted=true;
    if(!this.form.valid)return;
    this._apiService.Post('article','insertArticle',this.form.value).subscribe((res:any)=>{
      this.submitted=false;
      if(res.success){
        this._toastr.success(res.message);
        $("#exampleModalLong").modal('toggle');
        this.form.reset();
        this.getArticles();
      }
      else{
        this._toastr.error(res.message);
      }
    },err=>{
      this.submitted=false;
      this._toastr.error('Connection Problem');
    })
    
  }

  update(){
    this.submitted=true;
    if(!this.form.valid)return;
    this._apiService.Put('article','',this.form.value).subscribe((res:any)=>{
      this.submitted=false;
      if(res.success){
        this._toastr.success(res.message);
        $("#exampleModalLong").modal('toggle');
        this.form.reset();
        this.getArticles();
      }
      else{
        this._toastr.error(res.message);
      }
    },err=>{
      this.submitted=false;
      this._toastr.error('Connection Problem');
    })
  }

  viewArticle(article:any){
    this._router.navigateByUrl(`/article-detail/${article.id}`)
  }

  openDialogdeleteArticle(article:any){
    this.selectedArticle=article;
  }
  
  

  deleteArticle(){
    this._apiService.Post('article','deleteArticle',this.selectedArticle).subscribe((res:any)=>{
      if(res.success){
        this._toastr.success(res.message);
        this.getArticles();
        this.selectedArticle={};
      }
      else{
        this._toastr.error(res.message);
      }
    },err=>{
      // console.log(err)
      this._toastr.error('Connection Problem');
    })
  }

}
