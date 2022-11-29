import { NgModule     } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Routes       } from '@angular/router';

import { HomeComponent       } from './components';
import { AuthorizeGuard      } from './guards';
import { SignInCallbackGuard } from './guards';
import { SilentCallbackGuard } from './guards';

const routes: Routes = [
  {
    path: 'signin-callback',
    children: [],
    canActivate: [
      SignInCallbackGuard,
    ],
  },
  {
    path: 'silent-callback',
    children: [],
    canActivate: [
      SilentCallbackGuard,
    ],
  },
  {
    path: '',
    pathMatch: 'full',
    component: HomeComponent,
    canActivate: [
      AuthorizeGuard,
    ],
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
