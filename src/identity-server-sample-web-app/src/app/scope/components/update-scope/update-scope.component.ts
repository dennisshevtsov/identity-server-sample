import { Component } from '@angular/core';
import { OnDestroy } from '@angular/core';
import { OnInit    } from '@angular/core';

import { ActivatedRoute } from '@angular/router';
import { ParamMap       } from '@angular/router';

import { Subscription } from 'rxjs';
import { switchMap    } from 'rxjs';

import { ScopeViewModel       } from '../scope';
import { UpdateScopeViewModel } from './update-scope.view-model';

@Component({
  templateUrl: './update-scope.component.html',
  providers: [UpdateScopeViewModel],
})
export class UpdateScopeComponent implements OnInit, OnDestroy {
  private readonly sub: Subscription;

  public constructor(
    private readonly vm   : UpdateScopeViewModel,
    private readonly route: ActivatedRoute,
  ) {
    this.sub = new Subscription();
  }

  public ngOnInit(): void {
    const initialize = (params: ParamMap) =>  {
      this.vm.scope.scopeName = params.get('scopeName')!;

      return this.vm.initialize();
    };

    this.sub.add(
      this.route.paramMap.pipe(switchMap(initialize))
                         .subscribe());
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
