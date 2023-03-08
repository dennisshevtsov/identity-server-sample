import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { UserManager } from 'oidc-client';

import { from       } from 'rxjs';
import { Observable } from 'rxjs';
import { switchMap  } from 'rxjs';
import { AddClientRequestDto } from '../dtos/add-client-request.dto';

import { GetClientsResponseDto } from '../dtos/get-clients-response.dto';

@Injectable({
  providedIn: 'root'
})
export class ClientService {
  public constructor(
    private readonly um  : UserManager,
    private readonly http: HttpClient,
  ) { }

  public getClients(): Observable<GetClientsResponseDto> {
    return from(this.um.getUser()).pipe(switchMap(user => {
      const url = 'api/client';
      const options = {
        headers: {
          'Content-Type' : 'application/json',
          'Authorization': `Bearer ${user?.access_token}`,
        },
      };

      return this.http.get<GetClientsResponseDto>(url, options);
    }));
  }

  public addClient(requestDto: AddClientRequestDto): Observable<void> {
    return from(this.um.getUser()).pipe(switchMap(user => {
      const url = 'api/client';
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
