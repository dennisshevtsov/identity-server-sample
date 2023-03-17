import { Component } from '@angular/core';
import { OnDestroy } from '@angular/core';

import { ActivatedRoute } from '@angular/router';
import { Router         } from '@angular/router';

import { Subscription } from 'rxjs';

import { AddScopeViewModel } from './add-scope.view-model';

@Component({
  templateUrl: './add-scope.component.html',
  providers: [
    AddScopeViewModel,
    {
      provide: Subscription,
      useFactory: () => new Subscription(),
    },
  ],
})
export class AddScopeComponent implements OnDestroy {
  public constructor(
    public readonly vm: AddScopeViewModel,

    private readonly subscription: Subscription,
    private readonly router: Router,
    private readonly route: ActivatedRoute,
  ) { }

  public ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  public ok(): void {
    const commands = ['../'];
    const extras = {
      relativeTo: this.route,
    };

    this.subscription.add(
      this.vm.add().subscribe(() => this.router.navigate(commands, extras)));
  }
}
