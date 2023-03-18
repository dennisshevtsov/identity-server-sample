import { Component    } from '@angular/core';
import { OnDestroy    } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { Input        } from '@angular/core';
import { OnInit       } from '@angular/core';
import { Output       } from '@angular/core';

import { FormArray    } from '@angular/forms';
import { FormBuilder, } from '@angular/forms';
import { FormControl, } from '@angular/forms';
import { FormGroup,   } from '@angular/forms';
import { Validators   } from '@angular/forms';

import { Subscription } from 'rxjs';

import { AudienceViewModel } from './audience.view-model';

type AudienceFormScheme = {
  [K in keyof AudienceViewModel]: AudienceViewModel[K] extends Array<infer T> ?
                                  FormArray<FormControl<T | null>> :
                                  FormControl<AudienceViewModel[K] | null>;
};

@Component({
  selector: 'app-audience',
  templateUrl: './audience.component.html',
})
export class AudienceComponent implements OnInit, OnDestroy {
  private readonly subscription: Subscription;
  private readonly okValue     : EventEmitter<void>;

  private audienceValue: undefined | AudienceViewModel;
  private formValue    : undefined | FormGroup<AudienceFormScheme>;

  public constructor(private readonly fb: FormBuilder) {
    this.subscription = new Subscription();
    this.okValue      = new EventEmitter<void>();
  }

  @Output()
  public get ok(): EventEmitter<void> {
    return this.okValue;
  }

  @Input()
  public set audience(value: AudienceViewModel) {
    this.audienceValue = value;
    this.form.setValue({
      audienceName: value.audienceName,
      displayName : value.displayName,
      description : value.description,
      scopes      : [...value.scopes],
    });
  }

  public ngOnInit(): void {
    this.form.valueChanges.subscribe(value => {
      this.audience.audienceName = value.audienceName ?? '';
      this.audience.displayName  = value.displayName ?? '';
      this.audience.description  = value.description ?? '';
    });
  }

  public ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  public get audience(): AudienceViewModel {
    return this.audienceValue ?? (this.audienceValue = new AudienceViewModel());
  }

  public get form(): FormGroup<AudienceFormScheme> {
    return this.formValue ?? (this.formValue = this.buildForm());
  }

  public addScope(): void {
    this.form.controls.scopes.push(this.fb.control(''));
  }

  public hasErrors(controlName: string): boolean {
    const control = this.form.get(controlName);

    return control != null && (!control.pristine || control.touched || control.dirty) && control.errors != null;
  }

  public hasError(controlName: string, errorCode: string): boolean {
    const control = this.form.get(controlName);

    return control != null && control.hasError(errorCode);
  }

  public save(): void {
    if (this.form.valid) {
      this.okValue.emit();
    }
  }

  private buildForm(): FormGroup<AudienceFormScheme> {
    return this.fb.group({
      audienceName: this.fb.control('', [Validators.required]),
      displayName : this.fb.control(''),
      description : this.fb.control(''),
      scopes      : this.fb.array(['']),
    });
  }
}
