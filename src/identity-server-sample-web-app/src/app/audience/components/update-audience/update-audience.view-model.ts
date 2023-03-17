import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { of         } from 'rxjs';

import { AudienceViewModel } from '../audience';

@Injectable()
export class UpdateAudienceViewModel {
  private audienceValue: undefined | AudienceViewModel;

  public get audience(): AudienceViewModel {
    return this.audienceValue ?? (this.audienceValue = new AudienceViewModel());
  }

  public update(): Observable<void> {
    return of(void 0);
  }
}