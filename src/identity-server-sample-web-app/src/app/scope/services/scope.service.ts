import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { UserManager } from 'oidc-client';

import { from       } from 'rxjs';
import { Observable } from 'rxjs';
import { switchMap  } from 'rxjs';

import { GetScopesResponseDto } from '../dtos/get-scopes-response.dto';

@Injectable({
  providedIn: 'root',
})
export class ScopeService {
  constructor(
    private readonly um  : UserManager,
    private readonly http: HttpClient,
  ) { }

  public getScopes(): Observable<GetScopesResponseDto> {
    return from(this.um.getUser()).pipe(switchMap(user => {
      const url = 'api/scope';
      const options = {
        headers: {
          'Content-Type' : 'application/json',
          'Authorization': `Bearer ${user!.access_token}`,
        },
      };

      return this.http.get<GetScopesResponseDto>(url, options)
    }))
  }
}
