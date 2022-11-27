import { Component, } from '@angular/core';

import { UserManager, } from 'oidc-client';

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
        authority: 'http://localhost:5085',
        client_id: 'identity-server-sample-api-client-id-1',
        redirect_uri: 'http://localhost:4202/sign-in-callback',
        response_type: 'code',
        scope: 'identity-server-sample-api-scope',
      }),
    },
  ],
})
export class AppComponent {
}
