import { Component    } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { Input        } from '@angular/core';
import { OnInit       } from '@angular/core';
import { Output       } from '@angular/core';

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
  private readonly okValue: EventEmitter<void>;

  private clientValue: undefined | ClientViewModel;
  private formValue  : undefined | FormGroup<ClientFormScheme>;

  public constructor(private readonly fb: FormBuilder) {
    this.okValue = new EventEmitter<void>();
  }

  public ngOnInit(): void {
    this.form.valueChanges.subscribe(value => {
      this.client.clientName = value.clientName ?? '';
      this.client.displayName = value.displayName ?? '';
      this.client.description = value.description ?? '';
    });
  }

  @Input()
  public set client(value: ClientViewModel) {
    this.clientValue = value;
    this.form.setValue({
      clientName      : value.clientName,
      displayName     : value.displayName,
      description     : value.description,
      scopes          : value.scopes,
      redirectUris    : value.redirectUris,
      postRedirectUris: value.postRedirectUris,
      corsOrigins     : value.corsOrigins,
    });
  }

  @Output()
  public get ok(): EventEmitter<void> {
    return this.okValue;
  }

  public get client(): ClientViewModel {
    return this.clientValue ?? (this.clientValue = new ClientViewModel());
  }

  public get form(): FormGroup<ClientFormScheme> {
    return this.formValue ?? (this.formValue = this.buildForm());
  }

  public save(): void {
    if (this.form.valid) {
      this.okValue.emit();
    }
  }

  public hasErrors(controlName: string): boolean {
    const control = this.form.get(controlName);

    return control != null && (!control.pristine || control.touched || control.dirty) && control.errors != null;
  }

  public hasError(controlName: string, errorCode: string): boolean {
    const control = this.form.get(controlName);

    return control != null && control.hasError(errorCode);
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
