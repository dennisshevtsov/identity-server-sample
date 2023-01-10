import { Component } from '@angular/core';

import { SearchAudiencesViewModel } from './search-audiences.view-model';

@Component({
  templateUrl: './search-audiences.component.html',
  providers: [SearchAudiencesViewModel],
})
export class SearchAudiencesComponent { }
