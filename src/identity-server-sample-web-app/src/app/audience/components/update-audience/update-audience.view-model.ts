import { Injectable } from '@angular/core';

import { map        } from 'rxjs';
import { Observable } from 'rxjs';

import { UpdateAudienceRequestDto } from '../../dtos';
import { AudienceService          } from '../../services';
import { AudienceViewModel        } from '../audience';

@Injectable()
export class UpdateAudienceViewModel {
  private audienceValue: undefined | AudienceViewModel;

  public constructor(private readonly service: AudienceService) {}

  public get audience(): AudienceViewModel {
    return this.audienceValue ?? (this.audienceValue = new AudienceViewModel());
  }

  public setAudienceName(audienceName: string): UpdateAudienceViewModel {
    this.audience.audienceName = audienceName;

    return this;
  }

  public initialize(): Observable<void> {
    return this.service.getAudience(this.audience.audienceName)
                       .pipe(map(responseDto => {
                         this.audienceValue = new AudienceViewModel(
                           responseDto.audienceName,
                           responseDto.displayName,
                           responseDto.description,
                           [...responseDto.scopes],
                         );
                       }));
  }

  public update(): Observable<void> {
    const requestDto = new UpdateAudienceRequestDto(
      this.audience.audienceName,
      this.audience.displayName,
      this.audience.description,
      [...this.audience.scopes],
    );

    return this.service.updateAudience(requestDto);
  }
}