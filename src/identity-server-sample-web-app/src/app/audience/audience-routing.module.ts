import { NgModule } from '@angular/core';

import { RouterModule } from '@angular/router';
import { Routes       } from '@angular/router';

import { AddAudienceComponent     } from './components';
import { SearchAudiencesComponent } from './components';

const routes: Routes = [
  {
    path: '',
    component: SearchAudiencesComponent,
  },
  {
    path: 'new',
    component: AddAudienceComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AudienceRoutingModule { }
