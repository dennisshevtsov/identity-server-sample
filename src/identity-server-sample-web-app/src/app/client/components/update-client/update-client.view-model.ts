import { Injectable } from '@angular/core';

import { map        } from 'rxjs';
import { Observable } from 'rxjs';
import { of         } from 'rxjs';

import { ClientViewModel      } from '../client';
import { ClientService        } from '../../services';
import { GetClientRequestDto  } from '../../dtos';
import { GetClientResponseDto } from '../../dtos';

@Injectable()
export class UpdateClientViewModel {
  private clientValue: undefined | ClientViewModel;

  public constructor(public service: ClientService) {}

  public get client(): ClientViewModel {
    return this.clientValue ?? (this.clientValue = new ClientViewModel());
  }

  public initialize(): Observable<void> {
    const requestDto = new GetClientRequestDto(this.client.clientName);
    const fillClient = (responseDto: GetClientResponseDto) => {
      this.clientValue = new ClientViewModel(
        responseDto.clientName,
        responseDto.displayName,
        responseDto.description,
        [...responseDto.scopes],
        [...responseDto.redirectUris],
        [...responseDto.postRedirectUris],
        [...responseDto.corsOrigins]);
    }

    return this.service.getClient(requestDto)
                       .pipe(map(fillClient));
  }

  public update(): Observable<void> {
    return of(void 0);
  }
}
