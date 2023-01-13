import { NgModule } from '@angular/core';

import { RouterModule } from '@angular/router';
import { Routes       } from '@angular/router';

import { SearchClientsComponent } from './components';

const routes: Routes = [
  {
    path: '',
    component: SearchClientsComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ClientRoutingModule { }