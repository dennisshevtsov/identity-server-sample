import { Component } from '@angular/core';
import { Input     } from '@angular/core';

import { AudienceViewModel } from './audience.view-model';

@Component({
  selector: 'app-audience',
  templateUrl: './audience.component.html',
})
export class AudienceComponent {
  private audienceValue: undefined | AudienceViewModel;

  @Input()
  public set audience(value: AudienceViewModel) {
    this.audienceValue = this.audience;
  }

  public get audience(): AudienceViewModel {
    return this.audienceValue ?? (this.audienceValue = new AudienceViewModel());
  }
}
