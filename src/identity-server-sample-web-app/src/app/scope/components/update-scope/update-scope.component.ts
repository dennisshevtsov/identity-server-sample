import { Component } from '@angular/core';
import { OnDestroy } from '@angular/core';

import { Subscription } from 'rxjs';

import { ScopeViewModel       } from '../scope';
import { UpdateScopeViewModel } from './update-scope.view-model';

@Component({
  templateUrl: './update-scope.component.html',
  providers: [UpdateScopeViewModel],
})
export class UpdateScopeComponent implements OnDestroy {
  private readonly sub: Subscription;

  public constructor(private readonly vm: UpdateScopeViewModel) {
    this.sub = new Subscription();
  }

  public ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  public get scope(): ScopeViewModel {
    return this.vm.scope;
  }

  public ok(): void {
    this.sub.add(this.vm.update().subscribe());
  }
}
