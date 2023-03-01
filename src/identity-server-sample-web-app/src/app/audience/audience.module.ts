import { NgModule     } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SearchAudiencesComponent } from './componets';

import { AudienceRoutingModule } from './audience-routing.module';

@NgModule({
  declarations: [
    SearchAudiencesComponent,
  ],
  imports: [
    CommonModule,
    AudienceRoutingModule,
  ],
})
export class AudienceModule { }
