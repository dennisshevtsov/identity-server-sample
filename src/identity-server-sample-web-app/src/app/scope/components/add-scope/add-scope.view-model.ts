import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { AddScopeRequestDto } from '../../dtos';
import { ScopeService       } from '../../services';

import { ScopeViewModel } from '../scope';

@Injectable()
export class AddScopeViewModel {
  private scopeValue: undefined | ScopeViewModel;

  public constructor(private readonly service: ScopeService) { }

  public get scope(): ScopeViewModel {
    return this.scopeValue ?? (this.scopeValue = new ScopeViewModel());
  }

  public add(): Observable<void> {
    const requestDto = new AddScopeRequestDto(
      this.scope.scopeName,
      this.scope.displayName,
    );

    return this.service.addScope(requestDto);
  }
}
