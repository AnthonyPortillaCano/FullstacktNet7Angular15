import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Usuario } from '../models/usuario';
import { Constants } from '../utils/constants';
import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
   constructor(private router:Router,private loginService:LoginService){}
   public email:string='';
   public clave:string='';
   public mensaje:string='';
   public isError=false;
   logIn(formLogin:NgForm)
  {
       if(formLogin.valid)
       {
          let usu=new Usuario(this.email,this.clave,'');
          this.loginService.ValidarUsuario(usu).subscribe({
            next:(usuario:Usuario)=>{
               this.router.navigate(["mfacade/facade/minicio/inicio"]);
               this.isError=false;
            },error:(error)=>{
               this.isError=true;
               this.mensaje=Constants.MENSAJE_ERROR.LOGIN.DATOS_ERRONEOS;
               console.log(error);
               setTimeout(()=>{this.isError=false;},4000);
            }
          })
       }
       else
       {
          this.isError=true;
          this.mensaje=Constants.MENSAJE_ERROR.LOGIN.CREDENCIALES;
          setTimeout(()=>{this.isError=false;},4000);
       }
  }
}
