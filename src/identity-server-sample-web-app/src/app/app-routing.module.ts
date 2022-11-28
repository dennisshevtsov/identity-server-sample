import { NgModule             } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { SignInComponent,     } from './components';
import { SignInCallbackGuard, } from "./guards";

const routes: Routes = [
  {
    path: 'sign-in-callback',
    canActivate: [
      SignInCallbackGuard,
    ],
    children: [],
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
