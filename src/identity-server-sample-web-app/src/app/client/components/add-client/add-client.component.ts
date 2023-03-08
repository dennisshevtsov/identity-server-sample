import { Component } from '@angular/core';

import { Subscription } from 'rxjs';

import { ActivatedRoute } from '@angular/router';
import { Router         } from '@angular/router';

import { AddClientViewModel } from './add-client.view-model';

@Component({
  templateUrl: './add-client.component.html',
  providers: [
    AddClientViewModel,
    {
      provide: Subscription,
      useFactory: () => new Subscription(),
    },
  ],
})
export class AddClientComponent {
  public constructor(
    public readonly vm: AddClientViewModel,

    private readonly subscription: Subscription,

    private readonly router: Router,
    private readonly route : ActivatedRoute,
  ) {}

  public ok(): void {
    const commands = ['../'];
    const extras   = {
      relativeTo: this.route,
    };

    this.subscription.add(
      this.vm.add().subscribe(() =>
        this.router.navigate(commands, extras)));
  }
}
