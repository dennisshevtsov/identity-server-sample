import { Component } from '@angular/core';
import { OnDestroy } from '@angular/core';

import { Subscription } from 'rxjs';

import { AudienceViewModel    } from '../audience/audience.view-model';
import { AddAudienceViewModel } from './add-audience.view-model';

@Component({
  templateUrl: './add-audience.component.html',
  providers: [AddAudienceViewModel]
})
export class AddAudienceComponent implements OnDestroy {
  private readonly sub: Subscription;

  public constructor(private readonly vm: AddAudienceViewModel) {
    this.sub = new Subscription();
  }

  public ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  public get audience(): AudienceViewModel {
    return this.vm.audience;
  }

  public ok(): void {
    this.sub.add(this.vm.add().subscribe());
  }
}
