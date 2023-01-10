import { Component } from '@angular/core';

import { AddAudienceViewModel } from './add-audience.view-model';

@Component({
  templateUrl: './add-audience.component.html',
  providers: [AddAudienceViewModel],
})
export class AddAudienceComponent { }
