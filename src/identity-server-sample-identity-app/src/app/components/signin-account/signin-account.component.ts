import { DOCUMENT  } from '@angular/common';

import { Component, OnDestroy } from '@angular/core';
import { Inject    } from '@angular/core';
import { OnInit    } from '@angular/core';

import { FormBuilder } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { FormGroup   } from '@angular/forms';
import { Validators  } from '@angular/forms';

import { Subscription } from 'rxjs';

import { SigninAccountViewModel } from './signin-account.view-mode';

interface SinginForm {
  email   : FormControl<string | null>;
  password: FormControl<string | null>;
}

@Component({
  templateUrl: './signin-account.component.html',
  providers: [
    SigninAccountViewModel,
    {
      provide: Subscription,
      useFactory: () => new Subscription(),
    },
  ],
})
export class SigninComponent implements OnInit, OnDestroy {
  private formValue: undefined | FormGroup<SinginForm>;

  public constructor(
    @Inject(DOCUMENT)
    private readonly document: Document,

    private readonly fb : FormBuilder,
    private readonly sub: Subscription,

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

  public ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  public signin(): void {
    if (this.form.valid) {
      this.sub.add(
        this.vm.signin().subscribe(redirectUrl =>
          this.document.defaultView?.open(redirectUrl)));
    }
  }

  private buildForm(): FormGroup<SinginForm> {
    return this.fb.group({
      email   : this.fb.control('', Validators.required),
      password: this.fb.control('', Validators.required),
    });
  }
}
