import { Injectable } from '@angular/core';

import { ScopeViewModel } from '../scope';

@Injectable()
export class AddScopeViewModel {
  private scopeValue: undefined | ScopeViewModel;

  public get scope(): ScopeViewModel {
    return this.scopeValue ?? (this.scopeValue = new ScopeViewModel());
  }

  public add(): void {}
}
