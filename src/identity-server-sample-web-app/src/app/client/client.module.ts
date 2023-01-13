import { NgModule     } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AddClientComponent     } from './components';
import { UpdateClientComponent  } from './components';
import { SearchClientsComponent } from './components';
import { ClientComponent        } from './components';

import { ClientRoutingModule } from './client-routing.module';

@NgModule({
  declarations: [
    AddClientComponent,
    ClientComponent,
    SearchClientsComponent,
    UpdateClientComponent,
  ],
  imports: [
    CommonModule,
    ClientRoutingModule,
  ]
})
export class ClientModule { }
