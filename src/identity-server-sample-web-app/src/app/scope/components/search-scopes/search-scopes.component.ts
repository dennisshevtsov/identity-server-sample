import { Component } from '@angular/core';
import { OnDestroy } from '@angular/core';
import { OnInit    } from '@angular/core';

import { Subscription } from 'rxjs';

import { SearchScopesViewModel } from './search-scopes.view-model';

@Component({
  templateUrl: './search-scopes.component.html',
  providers: [
    SearchScopesViewModel,
    {
      provide: Subscription,
      useFactory: () => new Subscription(),
    },
  ],
})
export class SearchScopesComponent implements OnInit, OnDestroy {
  public constructor(
    public readonly vm: SearchScopesViewModel,

    private readonly sub: Subscription,
  ) { }

  public ngOnInit(): void {
    this.sub.add(this.vm.initialize().subscribe());
  }

  public ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}
