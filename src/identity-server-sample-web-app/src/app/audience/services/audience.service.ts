import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { AddAudienceRequestDto   } from '../dtos';
import { GetAudienceResponseDto  } from '../dtos';
import { GetAudiencesResponseDto } from '../dtos';

@Injectable({
  providedIn: 'root'
})
export class AudienceService {
  public constructor(private readonly http: HttpClient) {}

  public getAudiences(): Observable<GetAudiencesResponseDto> {
    return this.http.get<GetAudiencesResponseDto>('api/audience');
  }

  public getAudience(audienceName: string): Observable<GetAudienceResponseDto> {
    return this.http.get<GetAudienceResponseDto>(`api/audience/${audienceName}`);
  }

  public addAudience(requestDto: AddAudienceRequestDto): Observable<void> {
    const url = 'api/audience';
    const body = JSON.stringify(requestDto);

    return this.http.post<void>(url, body);
  }
}
