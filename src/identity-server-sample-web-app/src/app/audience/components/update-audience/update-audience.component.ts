import { Component } from '@angular/core';
import { OnDestroy } from '@angular/core';

import { Subscription } from 'rxjs';

import { AudienceViewModel       } from '../audience';
import { UpdateAudienceViewModel } from './update-audience.view-model';

@Component({
  templateUrl: './update-audience.component.html',
  providers: [UpdateAudienceViewModel],
})
export class UpdateAudienceComponent implements OnDestroy {
  private sub: Subscription;

  public constructor(private readonly vm: UpdateAudienceViewModel) {
    this.sub = new Subscription();
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
