import { ClientViewModel } from '../client/client.view-model';

export class AddClientViewModel {
  private clientValue: undefined | ClientViewModel;

  public get client(): ClientViewModel {
    return this.clientValue ?? (this.clientValue = new ClientViewModel());
  }

  public add(): void { }
}
