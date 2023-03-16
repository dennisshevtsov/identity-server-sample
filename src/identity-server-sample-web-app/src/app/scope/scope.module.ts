import { NgModule            } from '@angular/core';
import { CommonModule        } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { SearchScopesComponent } from './components';
import { AddScopeComponent     } from './components';
import { ScopeComponent        } from './components';

import { ScopeRoutingModule } from './scope-routing.module';
import { UpdateScopeComponent } from './components/update-scope/update-scope.component';

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
export class ScopeModule { }
