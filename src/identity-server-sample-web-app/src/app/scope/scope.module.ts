import { NgModule            } from '@angular/core';
import { CommonModule        } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

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
    ReactiveFormsModule,
    ScopeRoutingModule,
  ],
})
export class ScopeModule { }
