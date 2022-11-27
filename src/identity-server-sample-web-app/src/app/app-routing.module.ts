import { NgModule             } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { SignInCallbackComponent, SignInComponent, } from './components';

const routes: Routes = [
  {
    path: 'sign-in-callback',
    component: SignInCallbackComponent,
  },
  {
    path: '',
    component: SignInComponent,
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
  ],
  exports: [
    RouterModule,
  ],
})
export class AppRoutingModule { }
