import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, catchError, Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Usuario } from '../models/usuario';

const httpOptions={
  headers:new HttpHeaders({
    'Content-Type':'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class LoginService {
   private userSubject:BehaviorSubject<Usuario | null>
   public user:Observable<Usuario | null>

  constructor(private http:HttpClient,private router:Router) 
  { 
     this.userSubject=new BehaviorSubject(JSON.parse(localStorage.getItem('user')));
     this.user=this.userSubject.asObservable();
  }
   public get userValue(){
    return this.userSubject.value;
   }
   private readonly API_URL=environment.apiUrl;
   public ValidarUsuario(usuario:Usuario):Observable<Usuario>{
       return this.http.post<Usuario>(this.API_URL+"/Api/Users/ValidateUser",usuario,httpOptions).pipe(tap((usuario:Usuario)=>{
           console.log(usuario);
           localStorage.setItem('email',usuario.email);
           localStorage.setItem('password',usuario.password);
           localStorage.setItem('user',JSON.stringify(usuario));
           this.userSubject.next(usuario);
           return usuario;

       }),catchError(err=>{throw console.log("Error del servidor detalles"+err)}))
   }
   logout()
   {
    localStorage.removeItem('user');
    localStorage.removeItem('error');
     this.userSubject.next(null);
     this.router.navigate(['mlogin/login']);
   }
}
