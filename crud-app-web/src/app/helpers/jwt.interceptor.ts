import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment.development";
import { LoginService } from "../login/login.service";

@Injectable()
export class JwtInterceptor implements HttpInterceptor{
    constructor(private loginService:LoginService){}
    intercept(request:HttpRequest<any>,next:HttpHandler):Observable<HttpEvent<any>>{
         const user=this.loginService.userValue;
         const isLoggedIn=user && user.token;
         const isApiUrl=request.url.startsWith(environment.apiUrl);
         if(isLoggedIn && isApiUrl)
         {
            request=request.clone({
              setHeaders:{
                Authorization:`Bearer ${user.token}`
              }
            });
         }
         return next.handle(request);
    }
}