import { Injectable } from '@angular/core';

import { map        } from 'rxjs';
import { Observable } from 'rxjs';

import { UpdateScopeRequestDto } from '../../dtos';
import { ScopeService          } from '../../services';
import { ScopeViewModel        } from '../scope';

@Injectable()
export class UpdateScopeViewModel {
  private scopeValue: undefined | ScopeViewModel;

  public constructor(private readonly service: ScopeService) {}

  public get scope(): ScopeViewModel {
    return this.scopeValue ?? (this.scopeValue = new ScopeViewModel());
  }

  public initialize(): Observable<void> {
    return this.service.getScope(this.scope.scopeName)
                       .pipe(map(responseDto => {
                         this.scopeValue = new ScopeViewModel(
                           responseDto.scopeName,
                           responseDto.displayName,
                         );
                       }));
  }

  public update(): Observable<void> {
    const requestDto = new UpdateScopeRequestDto(
      this.scope.scopeName,
      this.scope.displayName,
    );

    return this.service.updateScope(requestDto);
  }
}
