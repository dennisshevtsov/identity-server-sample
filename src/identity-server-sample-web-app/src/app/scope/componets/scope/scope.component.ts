import { Component    } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { Input        } from '@angular/core';
import { OnInit       } from '@angular/core';
import { Output       } from '@angular/core';

import { FormBuilder } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { FormGroup   } from '@angular/forms'
import { Validators  } from '@angular/forms';

import { ScopeViewModel } from './scope.view-model';

type ScopeFormScheme = {
  [K in keyof ScopeViewModel]: FormControl<ScopeViewModel[K] | null>;
};

@Component({
  selector: 'app-scope',
  templateUrl: './scope.component.html',
})
export class ScopeComponent implements OnInit {
  private readonly okValue: EventEmitter<void>;

  private formValue : undefined | FormGroup<ScopeFormScheme>;
  private scopeValue: undefined | ScopeViewModel;

  public constructor(private readonly fb: FormBuilder) {
    this.okValue = new EventEmitter<void>();
  }

  public get form(): FormGroup<ScopeFormScheme> {
    return this.formValue ?? (this.formValue = this.buildForm());
  }

  public get scope(): ScopeViewModel {
    return this.scopeValue ?? (this.scopeValue = new ScopeViewModel());
  }

  @Input()
  public set scope(value: ScopeViewModel) {
    this.scopeValue = value;
  }

  @Output()
  public get ok(): EventEmitter<void> {
    return this.okValue;
  }

  public ngOnInit(): void {
    this.form.valueChanges.subscribe(value => {
      this.scope.scopeName   = value.scopeName ?? '';
      this.scope.displayName = value.displayName ?? '';
    });
  }

  public save(): void {
    if (this.form.valid) {
      this.okValue.emit();
    }
  }

  public buildForm(): FormGroup<ScopeFormScheme> {
    return this.fb.group({
      scopeName  : this.fb.control('', Validators.required),
      displayName: this.fb.control(''),
    });
  }
}
