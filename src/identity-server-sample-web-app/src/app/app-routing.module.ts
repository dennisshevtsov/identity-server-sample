import { NgModule             } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { SignInComponent     } from './components';
import { SignInCallbackGuard } from './guards';
import { SilentCallbackGuard } from './guards';

const routes: Routes = [
  {
    path: 'signin-callback',
    canActivate: [
      SignInCallbackGuard,
    ],
    children: [],
  },
  {
    path: 'silent-callback',
    canActivate: [
      SilentCallbackGuard,
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
