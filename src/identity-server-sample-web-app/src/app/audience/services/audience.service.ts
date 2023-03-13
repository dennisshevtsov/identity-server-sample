import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { UserManager } from 'oidc-client';

import { from       } from 'rxjs';
import { Observable } from 'rxjs';
import { switchMap  } from 'rxjs';

import { AddAudienceRequestDto   } from '../dtos';
import { GetAudiencesResponseDto } from '../dtos';

@Injectable({
  providedIn: 'root'
})
export class AudienceService {
  public constructor(
    private readonly um  : UserManager,
    private readonly http: HttpClient,
  ) { }

  public getAudiences(): Observable<GetAudiencesResponseDto> {
    return from(this.um.getUser()).pipe(switchMap(user => {
      const url     = 'api/audience';
      const options = {
        headers: {
          'Content-Type' : 'application/json',
          'Authorization': `Bearer ${user!.access_token}`,
        },
      };

      return this.http.get<GetAudiencesResponseDto>(url, options);
    }));
  }

  public addAudience(requestDto: AddAudienceRequestDto): Observable<void> {
    return from(this.um.getUser()).pipe(switchMap(user => {
      const url = 'api/audience';
      const body = JSON.stringify(requestDto);
      const options = {
        headers: {
          'Content-Type' : 'application/json',
          'Authorization': `Bearer ${user?.access_token}`,
        },
      };

      return this.http.post<void>(url, body, options);
    }));
  }
}
