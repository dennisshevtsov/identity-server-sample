import { Injectable } from '@angular/core';

import { map        } from 'rxjs';
import { Observable } from 'rxjs';

import { ClientDto     } from '../../dtos/get-clients-response.dto';
import { ClientService } from '../../services/client.service';

@Injectable()
export class SearchClientsViewModel {
  private clientsValue: undefined | ClientDto[];

  public constructor(private readonly service: ClientService) { }

  public initialize(): Observable<void> {
    return this.service.getClients().pipe(map(responseDto => {
      this.clientsValue = responseDto.clients;
    }));
  }
}
