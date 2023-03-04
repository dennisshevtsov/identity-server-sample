import { Component } from '@angular/core';
import { Router    } from '@angular/router';

import { AddScopeViewModel } from './add-scope.view-model';

@Component({
  templateUrl: './add-scope.component.html',
  providers: [AddScopeViewModel],
})
export class AddScopeComponent {
  public constructor(
    public readonly vm: AddScopeViewModel,
    private readonly router: Router,
  ) { }

  public ok(): void {
    this.vm.add();
    this.router.navigate(['../']);
  }
}
