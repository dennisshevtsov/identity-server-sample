import { Component } from '@angular/core';
import { OnDestroy } from '@angular/core';
import { OnInit    } from '@angular/core';

import { ActivatedRoute } from '@angular/router';
import { ParamMap       } from '@angular/router';

import { mergeMap     } from 'rxjs';
import { Subscription } from 'rxjs';
import { throwError   } from 'rxjs';

import { ErrorViewModel } from './error.view-model';

@Component({
  templateUrl: './error.component.html',
  providers: [
    ErrorViewModel,
    {
      provide: Subscription,
      useFactory: () => new Subscription(),
    },
  ],
})
export class ErrorComponent implements OnInit, OnDestroy {
  public constructor(
    public readonly vm : ErrorViewModel,

    private readonly sub  : Subscription,
    private readonly route: ActivatedRoute,
  ) { }

  public ngOnInit(): void {
    const initialize = (params: ParamMap) => {
      const errorId = params.get('errorId');

      if (errorId) {
        this.vm.errorId = errorId;

        return this.vm.initialize();
      }

      return throwError(() => new Error());
    };

    this.sub.add(
      this.route.queryParamMap.pipe(mergeMap(initialize))
                              .subscribe());
  }

  public ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}
