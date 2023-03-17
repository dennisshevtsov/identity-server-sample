import { Component } from '@angular/core';
import { AudienceViewModel } from '../audience/audience.view-model';

@Component({
  templateUrl: './update-audience.component.html',
})
export class UpdateAudienceComponent {
  public get audience(): AudienceViewModel {
    return new AudienceViewModel();
  }

  public ok(): void {}
}
