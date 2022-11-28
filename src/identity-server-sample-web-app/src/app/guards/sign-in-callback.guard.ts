import { Injectable,             } from '@angular/core';
import { ActivatedRouteSnapshot,
         CanActivate,
         Router,
         RouterStateSnapshot,
         UrlTree,                } from '@angular/router';

import { UserManager, } from 'oidc-client';

@Injectable({
  providedIn: 'root',
})
export class SignInCallbackGuard implements CanActivate {
  public constructor(
    public userManager: UserManager,
    public router     : Router) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot)
    : Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.userManager.signinRedirectCallback()
                           .then(() => this.router.navigate(['']));
  }
}
