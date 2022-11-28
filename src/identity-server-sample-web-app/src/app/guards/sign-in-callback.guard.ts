import { Injectable,             } from '@angular/core';
import { ActivatedRouteSnapshot, } from '@angular/router';
import { CanActivate,            } from '@angular/router';
import { Router,                 } from '@angular/router';
import { RouterStateSnapshot,    } from '@angular/router';
import { UrlTree,                } from '@angular/router';

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
    : Promise<boolean | UrlTree> {
    return this.userManager.signinRedirectCallback()
                           .then(() => this.router.navigate(['']));
  }
}
