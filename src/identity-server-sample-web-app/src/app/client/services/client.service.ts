import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { AddClientRequestDto    } from '../dtos';
import { GetClientRequestDto    } from '../dtos';
import { GetClientResponseDto   } from '../dtos';
import { GetClientsResponseDto  } from '../dtos';
import { UpdateClientRequestDto } from '../dtos';

@Injectable({
  providedIn: 'root',
})
export class ClientService {
  public constructor(private readonly http: HttpClient) {}

  public getClients(): Observable<GetClientsResponseDto> {
    return this.http.get<GetClientsResponseDto>('api/client');
  }

  public getClient(requestDto: GetClientRequestDto): Observable<GetClientResponseDto> {
    return this.http.get<GetClientResponseDto>(`api/client/${requestDto.clientName}`);
  }

  public addClient(requestDto: AddClientRequestDto): Observable<void> {
    const url = 'api/client';
    const body = JSON.stringify(requestDto);

    return this.http.post<void>(url, body);
  }

  public updateClient(requestDto: UpdateClientRequestDto): Observable<void> {
    const url = `api/client/${requestDto.clientName}`;
    const body = JSON.stringify(requestDto);

    return this.http.put<void>(url, body);
  }
}
