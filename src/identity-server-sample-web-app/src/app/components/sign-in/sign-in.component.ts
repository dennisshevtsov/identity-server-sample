import { Component, } from '@angular/core';

import { UserManager, } from 'oidc-client';

@Component({
  templateUrl: './sign-in.component.html',
  styleUrls: [
    './sign-in.component.scss',
  ],
  providers: [
    {
      provide: UserManager,
      useFactory: () => new UserManager({
        authority: 'http://localhost:5085',
        client_id: 'identity-server-sample-api-client-id-1',
        redirect_uri: 'http://localhost:4202/sign-in-callback',
        response_type: 'code',
        scope: 'identity-server-sample-api-scope',
      }),
    },
  ],
})
export class SignInComponent {
  public constructor(
    private readonly userManager: UserManager) { }

  public signIn(): void {
    this.userManager.signinRedirect();
  }
}
