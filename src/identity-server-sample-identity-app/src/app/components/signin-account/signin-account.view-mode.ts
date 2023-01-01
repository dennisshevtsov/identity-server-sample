import { Injectable } from '@angular/core';
import { map        } from 'rxjs';
import { Observable } from 'rxjs';

import { SignInAccountRequestDto } from '../../dtos';
import { AccountService          } from '../../services';

@Injectable()
export class SigninAccountViewModel {
  private emailValue    : undefined | string;
  private passwordValue : undefined | string;
  private returnUrlValue: undefined | string;

  public constructor(
    private readonly service: AccountService) {}

  public get email(): string {
    return this.emailValue ?? '';
  }

  public set email(value: string) {
    this.emailValue = value;
  }

  public get password(): string {
    return this.passwordValue ?? '';
  }

  public set password(value: string) {
    this.passwordValue = value;
  }

  public get returnUrl(): string {
    return this.returnUrlValue ?? '';
  }

  public set returnUrl(value: string) {
    this.returnUrlValue = value;
  }

  public signin(): Observable<string> {
    const requestDto = new SignInAccountRequestDto(
      this.email, this.password, this.returnUrl);

    return this.service.signin(requestDto).pipe(
      map(responseDto => responseDto.redirectUrl));
  }
}
