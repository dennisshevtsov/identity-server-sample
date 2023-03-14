import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { AddAudienceRequestDto   } from '../dtos';
import { GetAudiencesResponseDto } from '../dtos';

@Injectable({
  providedIn: 'root'
})
export class AudienceService {
  public constructor(private readonly http: HttpClient) {}

  public getAudiences(): Observable<GetAudiencesResponseDto> {
    const url = 'api/audience';

    return this.http.get<GetAudiencesResponseDto>(url);
  }

  public addAudience(requestDto: AddAudienceRequestDto): Observable<void> {
    const url = 'api/audience';
    const body = JSON.stringify(requestDto);

    return this.http.post<void>(url, body);
  }
}
