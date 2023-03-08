import { Observable } from 'rxjs';

import { AddClientRequestDto } from '../../dtos/add-client-request.dto';
import { ClientService       } from '../../services/client.service';
import { ClientViewModel     } from '../client/client.view-model';

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
