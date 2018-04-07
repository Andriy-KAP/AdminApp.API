import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";
import { Observable } from "rxjs/Observable";
import { DataService } from "./data.service";
import { Injectable } from "@angular/core";

@Injectable()
export class CanActivateUserService implements CanActivate {
    constructor(private router: Router){

    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean> {
        if(DataService.getToken() != null){
            return true;
        }
        else{
            this.router.navigate(['login']);
        }
        return false;
    }
}