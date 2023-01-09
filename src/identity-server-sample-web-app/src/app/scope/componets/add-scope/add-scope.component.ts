import { Component } from '@angular/core';

import { AddScopeViewModel } from './add-scope.view-model';

@Component({
  selector: 'app-add-scope',
  templateUrl: './add-scope.component.html',
  providers: [AddScopeViewModel],
})
export class AddScopeComponent {
  public constructor(public readonly vm: AddScopeViewModel) { }
}
