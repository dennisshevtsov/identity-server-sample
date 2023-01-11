import { Injectable } from '@angular/core';

import { map        } from 'rxjs';
import { Observable } from 'rxjs';

import { AudienceDto     } from '../../dtos';
import { AudienceService } from '../../services';

@Injectable()
export class SearchAudiencesViewModel {
  private audiencesValue: undefined | AudienceDto[];

  public constructor(private readonly service: AudienceService) { }

  public get audiences(): AudienceDto[] {
    return this.audiencesValue ?? [];
  }

  public initialize(): Observable<void> {
    return this.service.getAudiences().pipe(map(responseDto => {
      this.audiencesValue = responseDto.audiences;
    }));
  }
}
