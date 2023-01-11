import { NgModule } from '@angular/core';

import { RouterModule } from '@angular/router';
import { Routes       } from '@angular/router';

import { SearchAudiencesComponent } from './componets';

const routers: Routes = [
  {
    path: 'audience',
    component: SearchAudiencesComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routers)],
  exports: [RouterModule],
})
export class AudienceRoutingModule { }
