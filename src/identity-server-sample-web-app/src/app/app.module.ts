import { BrowserModule    } from '@angular/platform-browser';
import { NgModule         } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { UserManager } from 'oidc-client';

import { AppComponent     } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { SignInComponent  } from './components';

@NgModule({
  declarations: [
    AppComponent,
    SignInComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
  ],
  providers: [
    {
      provide: UserManager,
      useFactory: () => new UserManager({
        authority: 'http://localhost:5085',
        client_id: 'identity-server-sample-api-client-id-1',
        redirect_uri: 'http://localhost:4202/signin-callback',
        silent_redirect_uri: 'http://localhost:4202/silent-callback',
        response_type: 'code',
        scope: 'identity-server-sample-api-scope',
      }),
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
