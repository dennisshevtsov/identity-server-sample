import { NgModule            } from '@angular/core';
import { CommonModule        } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { AudienceComponent        } from './components';
import { SearchAudiencesComponent } from './components';

import { AudienceRoutingModule } from './audience-routing.module';


@NgModule({
  declarations: [
    AudienceComponent,
    SearchAudiencesComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    AudienceRoutingModule,
  ],
})
export class AudienceModule { }
