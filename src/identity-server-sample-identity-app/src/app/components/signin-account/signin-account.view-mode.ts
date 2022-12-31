import { Observable } from 'rxjs';
import { of         } from 'rxjs';

export class SigninAccountViewModel {
  private emailValue   : undefined | string;
  private passwordValue: undefined | string;

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
    return of('');
  }
}
