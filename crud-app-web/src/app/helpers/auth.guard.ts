import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";
import { LoginService } from "../login/login.service";

@Injectable({providedIn:'root'})
export class AuthGuard implements CanActivate{
     constructor(private router:Router,private loginService:LoginService){}

    canActivate(route:ActivatedRouteSnapshot ,state:RouterStateSnapshot)
    {
        const user=this.loginService.userValue;
        if(user)
        {
            return true;
        }
        this.router.navigate(['mlogin/login'],{queryParams:{returnUrl:state.url}});
        return false;
    }
}