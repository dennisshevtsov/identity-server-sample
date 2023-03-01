import { BrowserModule    } from '@angular/platform-browser';
import { NgModule         } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AuthorizationModule } from './authorization';
import { AppComponent        } from './app.component';
import { AppRoutingModule    } from './app-routing.module';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    HttpClientModule,

    AuthorizationModule.forRoot(
      'http://localhost:5188',
      'http://localhost:4202',
      'identity-server-sample-api-client-id-1',
      'identity-server-sample-api-scope'),

    AppRoutingModule,
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
