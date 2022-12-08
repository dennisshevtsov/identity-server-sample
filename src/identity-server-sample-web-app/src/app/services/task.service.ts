import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { GetTasksResponseDto } from '../dtos';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  public constructor(private readonly http: HttpClient) { }

  public getTasks(): Observable<GetTasksResponseDto> {
    return this.http.get<GetTasksResponseDto>('task');
  }
}
