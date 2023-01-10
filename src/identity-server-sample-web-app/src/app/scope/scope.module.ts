import { NgModule     } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SearchScopesComponent } from './componets';
import { AddScopeComponent     } from './componets';
import { UpdateScopeComponent  } from './componets';
import { ScopeComponent        } from './componets';
import { ScopeRoutingModule } from './scope-routing.module';
import { AudienceModule } from '../audience/audience.module';

@NgModule({
  declarations: [
    SearchScopesComponent,
    AddScopeComponent,
    UpdateScopeComponent,
    AudienceModule,
    ScopeComponent,
  ],
  imports: [
    CommonModule,
    ScopeRoutingModule,
  ],
})
export class ScopeModule { }
