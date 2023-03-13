import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { AddAudienceRequestDto } from '../../dtos';
import { AudienceService       } from '../../services';
import { AudienceViewModel     } from '../audience/audience.view-model';

@Injectable()
export class AddAudienceViewModel {
  private audienceValue: undefined | AudienceViewModel;

  public constructor(public readonly service: AudienceService) {}

  public get audience(): AudienceViewModel {
    return this.audienceValue ?? (this.audienceValue = new AudienceViewModel());
  }

  public add(): Observable<void> {
    const requestDto = new AddAudienceRequestDto(
      this.audience.audienceName,
      this.audience.displayName,
      this.audience.description,
      this.audience.scopes,
    );

    return this.service.addAudience(requestDto);
  }
}
