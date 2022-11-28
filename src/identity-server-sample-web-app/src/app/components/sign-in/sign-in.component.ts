import { Component } from '@angular/core';

import { UserManager } from 'oidc-client';

@Component({
  templateUrl: './sign-in.component.html',
  styleUrls: [
    './sign-in.component.scss',
  ],
})
export class SignInComponent {
  public constructor(
    private readonly userManager: UserManager) { }

  public signIn(): void {
    this.userManager.signinRedirect();
  }
}
