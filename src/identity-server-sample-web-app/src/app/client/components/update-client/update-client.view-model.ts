import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { of         } from 'rxjs';

import { ClientViewModel } from '../client';
import { ClientService   } from '../../services';

@Injectable()
export class UpdateClientViewModel {
  private clientValue: undefined | ClientViewModel;

  public constructor(public service: ClientService) {}

  public get client(): ClientViewModel {
    return this.clientValue ?? (this.clientValue = new ClientViewModel());
  }

  public update(): Observable<void> {
    return of(void 0);
  }
}
