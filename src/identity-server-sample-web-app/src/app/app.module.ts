import { BrowserModule    } from '@angular/platform-browser';
import { NgModule         } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AuthorizationModule } from './authorization';
import { AppComponent        } from './app.component';
import { AppRoutingModule    } from './app-routing.module';
import { environment         } from '../environments/environment';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    HttpClientModule,

    AuthorizationModule.forRoot(
      environment.identityApiUrl,
      environment.appUrl,
      environment.clientId,
      environment.scope),

    AppRoutingModule,
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
