import { Component } from '@angular/core';

import { AddClientViewModel } from './add-client.view-model';

@Component({
  templateUrl: './add-client.component.html',
  providers: [AddClientViewModel],
})
export class AddClientComponent {
  public constructor(public readonly vm: AddClientViewModel) {}
}
