import { DOCUMENT  } from '@angular/common';

import { Component } from '@angular/core';
import { OnDestroy } from '@angular/core';
import { Inject    } from '@angular/core';
import { OnInit    } from '@angular/core';

import { FormBuilder } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { FormGroup   } from '@angular/forms';
import { Validators  } from '@angular/forms';

import { ActivatedRoute } from '@angular/router';

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
  private returnUrlValue: undefined | string;
  private formValue     : undefined | FormGroup<SinginForm>;

  public constructor(
    @Inject(DOCUMENT)
    private readonly document: Document,

    private readonly fb   : FormBuilder,
    private readonly route: ActivatedRoute,
    private readonly sub  : Subscription,

    public readonly vm: SigninAccountViewModel) {}

  public get form(): FormGroup<SinginForm> {
    return this.formValue ?? (this.formValue = this.buildForm());
  }

  public ngOnInit(): void {
    this.sub.add(this.route.queryParamMap.subscribe(params => {
      const returnUrl = params.get('returnUrl');

      if (returnUrl) {
        this.returnUrlValue = returnUrl;
      }
    }));

    this.sub.add(this.form.valueChanges.subscribe(value => {
      this.vm.email    = value.email    ?? '';
      this.vm.password = value.password ?? '';
    }));
  }

  public ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  public signin(): void {
    if (this.form.valid) {
      this.sub.add(
        this.vm.signin().subscribe(() => this.redirectBack()));
    }
  }

  private redirectBack(): void {
    if (this.document.defaultView && this.returnUrlValue) {
      this.document.defaultView.open(this.returnUrlValue, '_self');
    }
  }

  private buildForm(): FormGroup<SinginForm> {
    return this.fb.group({
      email   : this.fb.control('', Validators.required),
      password: this.fb.control('', Validators.required),
    });
  }
}
