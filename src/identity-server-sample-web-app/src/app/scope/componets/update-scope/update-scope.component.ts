import { Component } from '@angular/core';

import { UpdateScopeViewModel } from './update-scope.vew-model';

@Component({
  templateUrl: './update-scope.component.html',
  providers: [UpdateScopeViewModel],
})
export class UpdateScopeComponent { 
  public constructor(public readonly vm: UpdateScopeViewModel) { }
}
