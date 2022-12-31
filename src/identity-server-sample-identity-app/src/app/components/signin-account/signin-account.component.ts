import { Component   } from '@angular/core';
import { OnInit      } from '@angular/core';

import { FormBuilder } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { FormGroup   } from '@angular/forms';
import { Validators  } from '@angular/forms';

import { SigninAccountViewModel } from './signin-account.view-mode';

interface SinginForm {
  email   : FormControl<string | null>;
  password: FormControl<string | null>;
}

@Component({
  templateUrl: './signin-account.component.html',
  providers: [
    SigninAccountViewModel,
  ],
})
export class SigninComponent implements OnInit {
  private formValue: undefined | FormGroup<SinginForm>;

  public constructor(
    private readonly fb: FormBuilder,

    public readonly vm: SigninAccountViewModel) {}

  public get form(): FormGroup<SinginForm> {
    return this.formValue ?? (this.formValue = this.buildForm());
  }

  public ngOnInit(): void {
    this.form.valueChanges.subscribe(value => {
      this.vm.email    = value.email    ?? '';
      this.vm.password = value.password ?? '';
    });
  }

  public signin(): void {
    if (this.form.valid) {
      this.vm.signin();
    }
  }

  private buildForm(): FormGroup<SinginForm> {
    return this.fb.group({
      email   : this.fb.control('', Validators.required),
      password: this.fb.control('', Validators.required),
    });
  }
}
