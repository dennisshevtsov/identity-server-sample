import { ModuleWithProviders } from '@angular/core';
import { NgModule            } from '@angular/core';
import { CommonModule        } from '@angular/common';

import { UserManager } from 'oidc-client';

import { AuthorizationRoutingModule } from './authorization-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { HTTP_INTERCEPTOR_PROVIDER } from '../client/interceptors';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule,
    AuthorizationRoutingModule,
  ],
})
export class AuthorizationModule {
  public static forRoot(
    identityApiUrl: string, appUrl: string, clientId: string, scope: string)
    : ModuleWithProviders<AuthorizationModule> {
    return {
      ngModule: AuthorizationModule,
      providers: [
        HTTP_INTERCEPTOR_PROVIDER,
        {
          provide: UserManager,
          useFactory: () => new UserManager({
            authority               : identityApiUrl,
            client_id               : clientId,
            redirect_uri            : `${appUrl}/signin-callback`,
            silent_redirect_uri     : `${appUrl}/silent-callback`,
            post_logout_redirect_uri: appUrl,
            response_type           : 'code',
            scope                   : `openid profile ${scope}`,
          }),
        },
      ],
    };
  }
}
