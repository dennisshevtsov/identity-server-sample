import { Component } from '@angular/core';

import { Subscription } from 'rxjs';

import { AudienceViewModel    } from '../audience/audience.view-model';
import { AddAudienceViewModel } from './add-audience.view-model';

@Component({
  templateUrl: './add-audience.component.html',
  providers: [AddAudienceComponent]
})
export class AddAudienceComponent {
  private readonly sub: Subscription;

  public constructor(private readonly vm: AddAudienceViewModel) {
    this.sub = new Subscription();
  }

  public get audience(): AudienceViewModel {
    return this.vm.audience;
  }

  public ok(): void {
    this.sub.add(this.vm.add());
  }
}
