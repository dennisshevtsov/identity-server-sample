import { NgModule     } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SearchAudiencesComponent } from './components';

import { AudienceRoutingModule } from './audience-routing.module';
import { AudienceComponent } from './components/audience/audience.component';

@NgModule({
  declarations: [
    SearchAudiencesComponent,
    AudienceComponent,
  ],
  imports: [
    CommonModule,
    AudienceRoutingModule,
  ],
})
export class AudienceModule { }
