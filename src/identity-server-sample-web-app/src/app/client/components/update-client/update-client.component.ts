import { Component } from '@angular/core';

import { Subscription } from 'rxjs';

import { ClientViewModel       } from '../client';
import { UpdateClientViewModel } from './update-client.view-model';

@Component({
  templateUrl: './update-client.component.html',
  providers: [UpdateClientViewModel],
})
export class UpdateClientComponent {
  private readonly subscription: Subscription;

  public constructor(private readonly vm: UpdateClientViewModel) {
    this.subscription = new Subscription();
  }

  public get client(): ClientViewModel {
    return this.vm.client;
  }

  public ok(): void {
    this.subscription.add(this.vm.update().subscribe());
  }
}
