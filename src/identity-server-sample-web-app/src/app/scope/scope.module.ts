import { NgModule     } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SearchScopesComponent } from './componets';
import { AddScopeComponent     } from './componets';
import { UpdateScopeComponent  } from './componets';
import { ScopeComponent        } from './componets';

import { ScopeRoutingModule } from './scope-routing.module';

@NgModule({
  declarations: [
    SearchScopesComponent,
    AddScopeComponent,
    UpdateScopeComponent,
    ScopeComponent,
  ],
  imports: [
    CommonModule,
    ScopeRoutingModule,
  ],
})
export class ScopeModule { }
