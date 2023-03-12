import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { of         } from 'rxjs';

import { AudienceViewModel } from '../audience/audience.view-model';

@Injectable()
export class AddAudienceViewModel {
  private audienceValue: undefined | AudienceViewModel;

  public get audience(): AudienceViewModel {
    return this.audienceValue ?? (this.audienceValue = new AudienceViewModel());
  }

  public add(): Observable<void> {
    return of(void 0);
  }
}
