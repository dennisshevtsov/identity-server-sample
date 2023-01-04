import { NgModule     } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Routes       } from '@angular/router';

import { ErrorComponent  } from './components';
import { SigninComponent } from './components';

const routes: Routes = [
  {
    path     : 'error',
    component: ErrorComponent,
  },
  {
    path     : 'signin',
    component: SigninComponent,
  },
  {
    path      : '',
    pathMatch : 'full',
    redirectTo: 'signin',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
