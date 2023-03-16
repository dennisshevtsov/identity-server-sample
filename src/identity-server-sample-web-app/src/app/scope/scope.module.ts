import { NgModule            } from '@angular/core';
import { CommonModule        } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { AddScopeComponent     } from './components';
import { ScopeComponent        } from './components';
import { SearchScopesComponent } from './components';
import { UpdateScopeComponent  } from './components';

import { ScopeRoutingModule } from './scope-routing.module';

@NgModule({
  declarations: [
    SearchScopesComponent,
    AddScopeComponent,
    ScopeComponent,
    UpdateScopeComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ScopeRoutingModule,
  ],
})
export class ScopeModule {}
