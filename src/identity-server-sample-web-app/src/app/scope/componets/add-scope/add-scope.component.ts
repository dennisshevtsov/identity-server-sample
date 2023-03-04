import { Component } from '@angular/core';
import { OnDestroy } from '@angular/core';
import { Router    } from '@angular/router';

import { Subscription } from 'rxjs';

import { AddScopeViewModel } from './add-scope.view-model';

@Component({
  templateUrl: './add-scope.component.html',
  providers: [AddScopeViewModel, Subscription],
})
export class AddScopeComponent implements OnDestroy {
  public constructor(
    public readonly vm: AddScopeViewModel,

    private readonly subscription: Subscription,
    private readonly router: Router,
  ) { }

  public ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  public ok(): void {
    this.subscription.add(
      this.vm.add().subscribe(this.back));
  }

  private back(): void {
    this.router.navigate(['../']);
  }
}
