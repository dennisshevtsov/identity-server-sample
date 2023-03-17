import { NgModule } from '@angular/core';

import { RouterModule } from '@angular/router';
import { Routes       } from '@angular/router';

import { AddScopeComponent     } from './components';
import { SearchScopesComponent } from './components';
import { UpdateScopeComponent  } from './components';

const routes: Routes = [
  {
    path: '',
    component: SearchScopesComponent,
  },
  {
    path: 'new',
    component: AddScopeComponent,
  },
  {
    path: ':scopeName',
    component: UpdateScopeComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ScopeRoutingModule {}
