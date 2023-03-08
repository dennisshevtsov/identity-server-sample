import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { AddClientRequestDto } from '../../dtos';
import { ClientService       } from '../../services';
import { ClientViewModel     } from '../client/client.view-model';

@Injectable({
  providedIn: 'any',
})
export class AddClientViewModel {
  private clientValue: undefined | ClientViewModel;

  public constructor(public service: ClientService) {}

  public get client(): ClientViewModel {
    return this.clientValue ?? (this.clientValue = new ClientViewModel());
  }

  public add(): Observable<void> {
    const requestDto = new AddClientRequestDto(
      this.client.clientName,
      this.client.displayName,
      this.client.description,
      ['test'],
      ['test'],
      ['test'],
      ['test'],
    );

    return this.service.addClient(requestDto);
  }
}
