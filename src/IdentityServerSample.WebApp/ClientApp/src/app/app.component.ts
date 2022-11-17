import { Component, } from '@angular/core';

import { UserManager, } from 'oidc-client-ts';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: [
    './app.component.css',
  ],
  providers: [
    {
      provide: UserManager,
      useFactory: () => new UserManager({
        authority: 'http://localhost:5214',
        client_id: 'identity-server-sample-api-client-id-1',
        redirect_uri: 'http://localhost:4200',
        response_type: 'code',
        scope: 'identity-server-sample-api-scope',
      }),
    }
  ]
})
export class AppComponent {
  public constructor(
    private readonly um: UserManager) { }

  public signIn(): void {
    this.um.signinRedirect();
  }
}
