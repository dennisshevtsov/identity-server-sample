import { NgModule            } from '@angular/core';
import { CommonModule        } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { AddAudienceComponent     } from './components';
import { AudienceComponent        } from './components';
import { SearchAudiencesComponent } from './components';
import { UpdateAudienceComponent  } from './components';

import { AudienceRoutingModule } from './audience-routing.module';

@NgModule({
  declarations: [
    AddAudienceComponent,
    AudienceComponent,
    SearchAudiencesComponent,
    UpdateAudienceComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    AudienceRoutingModule,
  ],
})
export class AudienceModule {}
