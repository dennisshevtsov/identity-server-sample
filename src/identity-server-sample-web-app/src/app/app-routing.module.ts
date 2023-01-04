import { NgModule     } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Routes       } from '@angular/router';

import { HomeComponent       } from './components';
import { AuthorizeGuard      } from './guards';
import { SigninCallbackGuard } from './guards';
import { SilentCallbackGuard } from './guards';

const routes: Routes = [
  {
    path: 'signin-callback',
    children: [],
    canActivate: [SigninCallbackGuard],
  },
  {
    path: 'silent-callback',
    children: [],
    canActivate: [SilentCallbackGuard],
  },
  {
    path: '',
    pathMatch: 'full',
    canActivate: [AuthorizeGuard],
    component: HomeComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
