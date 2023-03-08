import { Injectable } from '@angular/core';

import { map        } from 'rxjs';
import { Observable } from 'rxjs';

import { ClientDto     } from '../../dtos';
import { ClientService } from '../../services';

@Injectable()
export class SearchClientsViewModel {
  private clientsValue: undefined | ClientDto[];

  public constructor(private readonly service: ClientService) { }

  public get clients(): ClientDto[] {
    return this.clientsValue ?? [];
  }

  public initialize(): Observable<void> {
    return this.service.getClients().pipe(map(responseDto => {
      this.clientsValue = responseDto.clients;
    }));
  }
}
