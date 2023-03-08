import { Component } from '@angular/core';
import { Input     } from '@angular/core';

import { ClientViewModel } from './client.view-model';

@Component({
  selector: 'app-client',
  templateUrl: './client.component.html',
})
export class ClientComponent {
  private clientValue: undefined | ClientViewModel;

  @Input()
  public set client(value: ClientViewModel) {
    this.clientValue = value;
  }
}
