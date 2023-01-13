import { NgModule     } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Routes       } from '@angular/router';

import { AuthorizeGuard      } from './authorization';
import { SigninCallbackGuard } from './authorization';
import { SilentCallbackGuard } from './authorization';

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
  {
    path: 'client',
    canLoad: [AuthorizeGuard],
    loadChildren: () => import('./client/client.module').then(module => module.ClientModule),
  },
  {
    path: 'scope',
    canLoad: [AuthorizeGuard],
    loadChildren: () => import('./scope/scope.module').then(module => module.ScopeModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
