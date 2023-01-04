import { Injectable  } from '@angular/core';
import { CanActivate } from '@angular/router';
import { Router      } from '@angular/router';
import { UrlTree     } from '@angular/router';

import { UserManager } from 'oidc-client';

@Injectable({
  providedIn: 'root',
})
export class SigninCallbackGuard implements CanActivate {
  public constructor(
    public userManager: UserManager,
    public router     : Router) { }

  public canActivate(): Promise<UrlTree> {
    return this.userManager.signinRedirectCallback()
                           .then(() => this.router.createUrlTree(['']));
  }
}
