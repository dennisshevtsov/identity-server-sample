import { NgModule } from '@angular/core';

import { RouterModule } from '@angular/router';
import { Routes       } from '@angular/router';

import { SearchAudiencesComponent } from './componets';

const routes: Routes = [
  {
    path: '',
    component: SearchAudiencesComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AudienceRoutingModule { }
