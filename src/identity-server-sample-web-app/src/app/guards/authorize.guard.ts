import { Injectable  } from '@angular/core';
import { CanActivate } from '@angular/router';

import { User        } from 'oidc-client';
import { UserManager } from 'oidc-client';

@Injectable({
  providedIn: 'root',
})
export class AuthorizeGuard implements CanActivate {
  public constructor(private readonly userManager: UserManager) { }

  public canActivate(): Promise<boolean> {
    return this.userManager.getUser().then(user => this.isAuthorized(user))
  }

  private isAuthorized(user: User | null): boolean | Promise<boolean> {
    if (user) {
      return true;
    }

    return this.userManager.signinRedirect().then(() => false);
  }
}
