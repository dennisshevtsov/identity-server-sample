import { Component } from '@angular/core';
import { OnDestroy } from '@angular/core';
import { OnInit    } from '@angular/core';

import { Subscription } from 'rxjs';

import { SearchAudiencesViewModel } from './search-audiences.view-model';

@Component({
  templateUrl: './search-audiences.component.html',
  providers: [
    SearchAudiencesViewModel,
    {
      provide: Subscription,
      useFactory: () => new Subscription(),
    },
  ],
})
export class SearchAudiencesComponent implements OnInit, OnDestroy {
  public constructor(
    public readonly vm: SearchAudiencesViewModel,

    private readonly sub: Subscription,
  ) { }

  public ngOnInit(): void {
    this.sub.add(this.vm.initialize().subscribe());
  }

  public ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}
