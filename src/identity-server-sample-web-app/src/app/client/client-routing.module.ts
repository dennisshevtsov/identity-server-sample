import { NgModule } from '@angular/core';

import { RouterModule } from '@angular/router';
import { Routes       } from '@angular/router';

import { AddClientComponent     } from './components';
import { SearchClientsComponent } from './components';

const routes: Routes = [
  {
    path: '',
    component: SearchClientsComponent,
  },
  {
    path: 'new',
    component: AddClientComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ClientRoutingModule { }