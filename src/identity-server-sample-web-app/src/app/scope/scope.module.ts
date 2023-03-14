import { NgModule            } from '@angular/core';
import { CommonModule        } from '@angular/common';
import { HttpClientModule    } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

import { SearchScopesComponent } from './componets';
import { AddScopeComponent     } from './componets';
import { ScopeComponent        } from './componets';

import { ScopeRoutingModule } from './scope-routing.module';

@NgModule({
  declarations: [
    SearchScopesComponent,
    AddScopeComponent,
    ScopeComponent,
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    ScopeRoutingModule,
  ],
})
export class ScopeModule { }
