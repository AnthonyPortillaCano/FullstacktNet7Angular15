import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { catchError, Observable, throwError } from "rxjs";
import { DialogoConfirmacionComponent } from "../dialogo-confirmacion/dialogo-confirmacion.component";
import { LoginService } from "../login/login.service";
import { Usuario } from "../models/usuario";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private loginService: LoginService,public dialogo:MatDialog) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            if ([401, 403].includes(err.status) && this.loginService.userValue) {
                // auto logout if 401 or 403 response returned from api
               
                this.dialogo.open(DialogoConfirmacionComponent,{
                    data : `¿Estas seguro de deseas permanecer en la sesion?`
                   }).afterClosed()
                   .subscribe((confirmado:Boolean)=>{
                       if(confirmado)
                       {
                        var user:any=JSON.parse(localStorage.getItem('user'));
                     
                        let usu=new Usuario(user.email,user.password,'');
                        this.loginService.ValidarUsuario(usu).subscribe({
                          next:(usuario:Usuario)=>{
                            setInterval("location.reload()",60000);
                            console.log(usuario);
                          },error:(error)=>{
                             
                          }
                        }) 
                       }
                       else
                       {
                        this.loginService.logout();
                       }
                   })
               // this.loginService.logout();
            }

            const error = err.error?.message || err.statusText;
             localStorage.setItem("error",JSON.stringify(err));
            return throwError(() => err);
        }))
    }
}