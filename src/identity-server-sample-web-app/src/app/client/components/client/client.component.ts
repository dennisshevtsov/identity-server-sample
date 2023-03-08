import { Component, OnInit } from '@angular/core';
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
export class ClientComponent implements OnInit {
  private clientValue: undefined | ClientViewModel;
  private formValue  : undefined | FormGroup<ClientFormScheme>;

  public constructor(private readonly fb: FormBuilder) {}

  public ngOnInit(): void {
    this.form.valueChanges.subscribe(value => {
      this.client.clientName = value.clientName ?? '';
      this.client.displayName = value.displayName ?? '';
      this.client.description = value.description ?? '';

      if (value.scopes) {
        this.client.scopes = value.scopes.filter(scope => scope)
                                         .map(scope => scope!);
      }

      if (value.redirectUris) {
        this.client.redirectUris = value.redirectUris.filter(redirectUri => redirectUri)
                                         .map(redirectUri => redirectUri!);
      }

      if (value.postRedirectUris) {
        this.client.postRedirectUris = value.postRedirectUris.filter(postRedirectUri => postRedirectUri)
                                                             .map(postRedirectUri => postRedirectUri!);
      }

      if (value.corsOrigins) {
        this.client.corsOrigins = value.corsOrigins.filter(corsOrigin => corsOrigin)
                                                   .map(corsOrigin => corsOrigin!);
      }
    });
  }

  @Input()
  public set client(value: ClientViewModel) {
    this.clientValue = value;
  }

  public get client(): ClientViewModel {
    return this.clientValue ?? (this.clientValue = new ClientViewModel());
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
