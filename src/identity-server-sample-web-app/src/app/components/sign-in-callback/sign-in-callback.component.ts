import { Component, OnInit, } from '@angular/core';
import { Router,            } from '@angular/router';

import { UserManager, } from 'oidc-client';

@Component({
  templateUrl: './sign-in-callback.component.html',
  styleUrls: [
    './sign-in-callback.component.scss',
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
export class SignInCallbackComponent implements OnInit {
  public constructor(
    public readonly userManager: UserManager,
    public readonly router     : Router,
  ) { }

  public ngOnInit(): void {
    this.userManager.signinRedirectCallback().then(() =>
      this.router.navigate(['']));
  }
}
