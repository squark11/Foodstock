import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { AuthService } from "../services/auth.service";

@Injectable()

export class authorizedGuard implements CanActivate{
  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return this.auth.state;
  }
  
  constructor(private auth:AuthService){}
}
