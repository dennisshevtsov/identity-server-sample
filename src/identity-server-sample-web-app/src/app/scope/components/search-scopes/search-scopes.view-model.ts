import { Injectable } from '@angular/core';

import { map        } from 'rxjs';
import { Observable } from 'rxjs';

import { ScopeDto     } from '../../dtos/get-scopes-response.dto';
import { ScopeService } from '../../services';

@Injectable()
export class SearchScopesViewModel {
  private scopesValue: undefined | ScopeDto[];

  public constructor(private readonly service: ScopeService) { }

  public get scopes(): ScopeDto[] {
    return this.scopesValue ?? [];
  }

  public initialize(): Observable<void> {
    return this.service.getScopes().pipe(map(responseDto => {
                                     this.scopesValue = responseDto.scopes;
                                   }));
  }
}
