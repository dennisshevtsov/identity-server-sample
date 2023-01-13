import { NgModule     } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Routes       } from '@angular/router';

import { AuthorizeGuard } from './authorization';

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
    redirectTo: 'audience',
  },
  {
    path: 'audience',
    canLoad: [AuthorizeGuard],
    loadChildren: () => import('./audience/audience.module').then(module => module.AudienceModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
