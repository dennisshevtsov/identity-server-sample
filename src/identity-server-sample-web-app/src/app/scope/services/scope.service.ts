import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { AddScopeRequestDto   } from '../dtos';
import { GetScopesResponseDto } from '../dtos';

@Injectable({
  providedIn: 'root',
})
export class ScopeService {
  constructor(private readonly http: HttpClient) {}

  public addScope(requestDto: AddScopeRequestDto): Observable<void> {
    const url = 'api/scope';
    const body = JSON.stringify(requestDto);

    return this.http.post<void>(url, body);
  }

  public getScopes(): Observable<GetScopesResponseDto> {
    return this.http.get<GetScopesResponseDto>('api/scope');
  }
}
