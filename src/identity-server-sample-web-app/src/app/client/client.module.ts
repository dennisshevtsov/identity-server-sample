import { NgModule     } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AddClientComponent     } from './components';
import { SearchClientsComponent } from './components';

import { ClientRoutingModule } from './client-routing.module';

@NgModule({
  declarations: [
    AddClientComponent,
    SearchClientsComponent,
  ],
  imports: [
    CommonModule,
    ClientRoutingModule,
  ],
})
export class ClientModule { }
