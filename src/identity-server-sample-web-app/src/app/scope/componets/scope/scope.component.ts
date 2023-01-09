import { Component    } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { Input        } from '@angular/core';
import { Output       } from '@angular/core';

import { ScopeViewModel } from './scope.view-model';

@Component({
  selector: 'app-scope',
  templateUrl: './scope.component.html',
})
export class ScopeComponent {
  private readonly okValue: EventEmitter<void>;

  private scopeValue: undefined | ScopeViewModel;

  public constructor() {
    this.okValue = new EventEmitter<void>();
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

  public save(): void {
    this.okValue.emit();
  }
}
