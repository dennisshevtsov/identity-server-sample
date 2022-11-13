import { Component,              } from '@angular/core';
import { FormBuilder, FormGroup, } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: [
    './app.component.css',
  ],
})
export class AppComponent {
  private formValue: undefined | FormGroup;

  public constructor(private readonly fb: FormBuilder) { }

  public get form(): FormGroup {
    return this.formValue ?? (this.formValue = this.buildForm());
  }

  public onSignIn(): void {
    console.log(JSON.stringify(this.form.value));
  }

  private buildForm(): FormGroup {
    return this.fb.group({
      'email': '',
      'password': '',
    });
  }
}
