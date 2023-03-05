import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { UserManager } from 'oidc-client';

import { from       } from 'rxjs';
import { Observable } from 'rxjs';
import { switchMap  } from 'rxjs';

import { AddScopeRequestDto   } from '../dtos';
import { GetScopesResponseDto } from '../dtos';

@Injectable({
  providedIn: 'root',
})
export class ScopeService {
  constructor(
    private readonly um  : UserManager,
    private readonly http: HttpClient,
  ) { }

  public addScope(requestDto: AddScopeRequestDto): Observable<void> {
    return from(this.um.getUser()).pipe(switchMap(user => {
      const url = 'api/scope';
      const body = JSON.stringify(requestDto);
      const options = {
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${user!.access_token}`,
        },
      };

      return this.http.post<void>(url, body, options)
    }));
  }

  public getScopes(): Observable<GetScopesResponseDto> {
    return from(this.um.getUser()).pipe(switchMap(user => {
      const url = 'api/scope';
      const options = {
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${user!.access_token}`,
        },
      };

      return this.http.get<GetScopesResponseDto>(url, options)
    }));
  }
}
