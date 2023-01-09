import { NgModule     } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SearchScopesComponent } from './componets';
import { AddScopeComponent     } from './componets';
import { UpdateScopeComponent  } from './componets';
import { ScopeComponent        } from './componets';

@NgModule({
  declarations: [
    SearchScopesComponent,
    AddScopeComponent,
    UpdateScopeComponent,
    ScopeComponent,
  ],
  imports: [CommonModule]
})
export class ScopeModule { }
