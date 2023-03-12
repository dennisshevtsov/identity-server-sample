import { Component    } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { Input        } from '@angular/core';
import { Output       } from '@angular/core';

import { FormArray    } from '@angular/forms';
import { FormBuilder, } from '@angular/forms';
import { FormControl, } from '@angular/forms';
import { FormGroup,   } from '@angular/forms';
import { Validators   } from '@angular/forms';

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
export class AudienceComponent {
  private readonly okValue: EventEmitter<void>;

  private audienceValue: undefined | AudienceViewModel;
  private formValue    : undefined | FormGroup<AudienceFormScheme>;

  public constructor(private readonly fb: FormBuilder) {
    this.okValue = new EventEmitter<void>();
  }

  @Output()
  public get ok(): EventEmitter<void> {
    return this.okValue;
  }

  @Input()
  public set audience(value: AudienceViewModel) {
    this.audienceValue = value;
  }

  public get audience(): AudienceViewModel {
    return this.audienceValue ?? (this.audienceValue = new AudienceViewModel());
  }

  public get form(): FormGroup<AudienceFormScheme> {
    return this.formValue ?? (this.formValue = this.buildForm());
  }

  public save(): void {
    this.okValue.emit();
  }

  private buildForm(): FormGroup<AudienceFormScheme> {
    return this.fb.group({
      audienceName: this.fb.control('', [Validators.required]),
      displayName : this.fb.control(''),
      description : this.fb.control(''),
      scopes      : this.fb.array(new Array<string>()),
    });
  }
}
