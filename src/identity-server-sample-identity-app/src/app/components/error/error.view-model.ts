import { Injectable } from '@angular/core';

import { map        } from 'rxjs';
import { Observable } from 'rxjs';

import { GetErrorRequestDto } from '../../dtos';
import { ErrorService       } from '../../services';

@Injectable()
export class ErrorViewModel {
  private errorIdValue: undefined | string;
  private messageValue: undefined | string;

  public constructor(private readonly service: ErrorService) { }

  public get errorId(): string {
    return this.errorIdValue ?? '';
  }

  public set errorId(value: string) {
    this.errorIdValue = value;
  }

  public get messsage(): string {
    return this.messageValue ?? '';
  }

  public initialize(): Observable<void> {
    return this.service.getError(new GetErrorRequestDto(this.errorId))
                       .pipe(map(responseDto => {
                         this.messageValue = responseDto.message;
                       }));
  }
}
