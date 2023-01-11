import { NgModule     } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AddClientComponent     } from './components';
import { UpdateClientComponent  } from './components';
import { SearchClientsComponent } from './components';
import { ClientComponent        } from './components';

@NgModule({
  declarations: [
    AddClientComponent,
    ClientComponent,
    SearchClientsComponent,
    UpdateClientComponent,
  ],
  imports: [
    CommonModule,
  ]
})
export class ClientModule { }
