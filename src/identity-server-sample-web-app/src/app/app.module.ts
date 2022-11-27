import { BrowserModule,    } from '@angular/platform-browser';
import { NgModule,         } from '@angular/core';
import { HttpClientModule, } from '@angular/common/http';

import { AppComponent,            } from './app.component';
import { AppRoutingModule,        } from './app-routing.module';
import { SignInCallbackComponent, } from './components';
import { SignInComponent } from './components/sign-in/sign-in.component';

@NgModule({
  declarations: [
    AppComponent,
    SignInCallbackComponent,
    SignInComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
