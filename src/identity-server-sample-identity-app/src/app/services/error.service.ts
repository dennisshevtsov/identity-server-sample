import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { GetErrorRequestDto, GetErrorResponseDto } from '../dtos';

@Injectable({
  providedIn: 'root',
})
export class ErrorService {
  constructor(private readonly http: HttpClient) { }

  public getError(requestDto: GetErrorRequestDto) {
    return this.http.get<GetErrorResponseDto>(`api/error/${requestDto.errorId}`);
  }
}
