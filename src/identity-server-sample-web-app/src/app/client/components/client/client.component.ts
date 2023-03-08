import { Component } from '@angular/core';
import { Input     } from '@angular/core';

import { FormArray   } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { FormGroup   } from '@angular/forms';
import { Validators  } from '@angular/forms';

import { ClientViewModel } from './client.view-model';

type ClientFormScheme = {
  [K in keyof ClientViewModel]: ClientViewModel[K] extends Array<infer T> ?
                                FormArray<FormControl<T | null>> :
                                FormControl<ClientViewModel[K] | null>;
};

@Component({
  selector: 'app-client',
  templateUrl: './client.component.html',
})
export class ClientComponent {
  private clientValue: undefined | ClientViewModel;
  private formValue  : undefined | FormGroup<ClientFormScheme>;

  public constructor(private readonly fb: FormBuilder) {}

  @Input()
  public set client(value: ClientViewModel) {
    this.clientValue = value;
  }

  public get form(): FormGroup<ClientFormScheme> {
    return this.formValue ?? (this.formValue = this.buildForm());
  }

  private buildForm(): FormGroup<ClientFormScheme> {
    return this.fb.group({
      clientName      : this.fb.control('', [Validators.required]),
      displayName     : this.fb.control(''),
      description     : this.fb.control(''),
      scopes          : this.fb.array(new Array<string>()),
      redirectUris    : this.fb.array(new Array<string>()),
      postRedirectUris: this.fb.array(new Array<string>()),
      corsOrigins     : this.fb.array(new Array<string>()),
    });
  }
}
