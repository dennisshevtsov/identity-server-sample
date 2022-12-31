import { map, Observable } from 'rxjs';

import { SignInAccountRequestDto } from '../../dtos';
import { AccountService          } from '../../services';

export class SigninAccountViewModel {
  private emailValue   : undefined | string;
  private passwordValue: undefined | string;

  public constructor(private readonly service: AccountService) {}

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

  public signin(): Observable<string> {
    const requestDto = new SignInAccountRequestDto(
      this.email, this.password);

    return this.service.signin(requestDto).pipe(
      map(responseDto => responseDto.redirectUrl));
  }
}
