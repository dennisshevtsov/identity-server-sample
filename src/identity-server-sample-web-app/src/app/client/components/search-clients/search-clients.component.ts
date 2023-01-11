import { Component } from '@angular/core';
import { OnDestroy } from '@angular/core';
import { OnInit    } from '@angular/core';

import { Subscription } from 'rxjs';

import { SearchClientsViewModel } from './search-clients.view-model';

@Component({
  templateUrl: './search-clients.component.html',
  providers: [
    SearchClientsViewModel,
    {
      provide: Subscription,
      useFactory: () => new Subscription(),
    },
  ],
})
export class SearchClientsComponent implements OnInit, OnDestroy {
  public constructor(
    public readonly vm: SearchClientsViewModel,

    private readonly sub: Subscription,
  ) { }

  public ngOnInit(): void {
    this.sub.add(this.vm.initialize().subscribe());
  }

  public ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}
