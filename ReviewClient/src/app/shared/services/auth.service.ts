import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  //It's for doing all of the auth functions
  constructor(private _router:Router) { }

   islogin():boolean{
    if (localStorage.getItem('jwt') != null) return true;
    else return false;
   }

   getToken():string{
     if(localStorage.getItem('jwt') != null)
      return 'Bearer ' +localStorage.getItem('jwt') || '{}';
    else
      return '';
   }

   saveUser(data:any):boolean{
    localStorage.setItem('jwt',data.token);
    localStorage.setItem('user',JSON.stringify(data));
    return true;
   }
   logout(){
     localStorage.clear();
     this._router.navigateByUrl('/auth/login');
     return true;
   }
   getUser():any{
     return JSON.parse(localStorage.getItem('user') || '{}');
   }
   getUserId():string{
     var user=JSON.parse(localStorage.getItem('user') || '{}');
     return user.userId;
   }
  

}
