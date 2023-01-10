import { NgModule     } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AudienceComponent        } from './componets';
import { AddAudienceComponent     } from './componets';
import { UpdateAudienceComponent  } from './componets';
import { SearchAudiencesComponent } from './componets';

@NgModule({
  declarations: [
    AddAudienceComponent,
    AudienceComponent,
    SearchAudiencesComponent,
    UpdateAudienceComponent,
  ],
  imports: [CommonModule],
})
export class AudienceModule { }
