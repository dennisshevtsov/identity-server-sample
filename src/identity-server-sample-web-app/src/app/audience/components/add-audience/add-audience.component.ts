import { Component } from '@angular/core';
import { OnDestroy } from '@angular/core';

import { ActivatedRoute } from '@angular/router';
import { Router         } from '@angular/router';

import { Subscription } from 'rxjs';

import { AudienceViewModel    } from '../audience/audience.view-model';
import { AddAudienceViewModel } from './add-audience.view-model';

@Component({
  templateUrl: './add-audience.component.html',
  providers: [AddAudienceViewModel]
})
export class AddAudienceComponent implements OnDestroy {
  private readonly subscription: Subscription;

  public constructor(
    private readonly vm: AddAudienceViewModel,

    private readonly route : ActivatedRoute,
    private readonly router: Router,
  ) {
    this.subscription = new Subscription();
  }

  public ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  public get audience(): AudienceViewModel {
    return this.vm.audience;
  }

  public ok(): void {
    const commands = ['../'];
    const extras   = {
      relativeTo: this.route,
    };

    this.subscription.add(
      this.vm.add().subscribe(() =>
        this.router.navigate(commands, extras)));
  }
}
