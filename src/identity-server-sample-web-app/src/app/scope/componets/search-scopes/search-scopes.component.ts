import { Component } from '@angular/core';

import { SearchScopesViewModel } from './search-scopes.view-model';

@Component({
  templateUrl: './search-scopes.component.html',
  providers: [SearchScopesViewModel],
})
export class SearchScopesComponent {
  public constructor(public readonly vm: SearchScopesViewModel) { }
}
