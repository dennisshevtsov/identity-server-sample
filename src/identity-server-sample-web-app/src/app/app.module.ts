import { BrowserModule    } from '@angular/platform-browser';
import { NgModule         } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AuthorizationModule } from './authorization';
import { AppComponent        } from './app.component';
import { AppRoutingModule    } from './app-routing.module';
import { userManagerProvider } from './authorization/providers';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    HttpClientModule,

    AuthorizationModule,
    AppRoutingModule,
  ],
  providers: [userManagerProvider],
  bootstrap: [AppComponent]
})
export class AppModule { }
