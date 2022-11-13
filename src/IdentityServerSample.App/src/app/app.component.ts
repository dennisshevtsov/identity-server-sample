import { Component,              } from '@angular/core';
import { FormBuilder, FormGroup, } from '@angular/forms';
import { UserManager,            } from 'oidc-client-ts';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: [
    './app.component.css',
  ],
  providers: [
    {
      provide: UserManager,
      useFactory: () => new UserManager({
        authority: '',
        client_id: '',
        redirect_uri: '',
        response_type: 'code',
        scope: '',
      }),
    }
  ]
})
export class AppComponent {
  private formValue: undefined | FormGroup;

  public constructor(
    private readonly fb: FormBuilder,
    private readonly um: UserManager) { }

  public get form(): FormGroup {
    return this.formValue ?? (this.formValue = this.buildForm());
  }

  public onSignIn(): void {
    this.um.signinRedirect();
  }

  private buildForm(): FormGroup {
    return this.fb.group({
      'email': '',
      'password': '',
    });
  }
}
