import { Component } from '@angular/core';
import { OnInit    } from '@angular/core';
import { OnDestroy } from '@angular/core';

import { ActivatedRoute } from '@angular/router';
import { ParamMap       } from '@angular/router';

import { Subscription } from 'rxjs';
import { switchMap    } from 'rxjs';

import { AudienceViewModel       } from '../audience';
import { UpdateAudienceViewModel } from './update-audience.view-model';

@Component({
  templateUrl: './update-audience.component.html',
  providers: [UpdateAudienceViewModel],
})
export class UpdateAudienceComponent implements OnInit, OnDestroy {
  private sub: Subscription;

  public constructor(
    private readonly vm   : UpdateAudienceViewModel,
    private readonly route: ActivatedRoute) {
    this.sub = new Subscription();
  }

  public ngOnInit(): void {
    const initialize = (param: ParamMap) => {
      return this.vm.setAudienceName(param.get('audienceName')!)
                    .initialize();
    };

    this.sub.add(this.route.paramMap.pipe(switchMap(initialize)).subscribe());
  }

  public ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  public get audience(): AudienceViewModel {
    return this.vm.audience;
  }

  public ok(): void {
    this.sub.add(this.vm.update().subscribe());
  }
}
