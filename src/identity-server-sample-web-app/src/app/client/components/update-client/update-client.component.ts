import { Component } from '@angular/core';
import { OnInit    } from '@angular/core';

import { ActivatedRoute } from '@angular/router';
import { ParamMap       } from '@angular/router';

import { mergeMap    } from 'rxjs';
import {Subscription } from 'rxjs';

import { ClientViewModel       } from '../client';
import { UpdateClientViewModel } from './update-client.view-model';

@Component({
  templateUrl: './update-client.component.html',
  providers: [UpdateClientViewModel],
})
export class UpdateClientComponent implements OnInit {
  private readonly subscription: Subscription;

  public constructor(
    private readonly vm: UpdateClientViewModel,
    private route: ActivatedRoute) {
    this.subscription = new Subscription();
  }

  public ngOnInit(): void {
    const initialize = (param: ParamMap) => {
      this.vm.client.clientName = param.get('clientName')!;

      return this.vm.initialize();
    };

    this.subscription.add(
      this.route.paramMap.pipe(mergeMap(initialize))
                         .subscribe());
  }

  public get client(): ClientViewModel {
    return this.vm.client;
  }

  public ok(): void {
    this.subscription.add(this.vm.update().subscribe());
  }
}
