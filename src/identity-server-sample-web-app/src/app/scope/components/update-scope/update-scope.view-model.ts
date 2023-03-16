import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { of         } from 'rxjs';

import { ScopeViewModel } from '../scope';

@Injectable()
export class UpdateScopeViewModel {
  private scopeValue: undefined | ScopeViewModel;

  public get scope(): ScopeViewModel {
    return this.scopeValue ?? (this.scopeValue = new ScopeViewModel());
  }

  public update(): Observable<void> {
    return of(void 0);
  }
}
