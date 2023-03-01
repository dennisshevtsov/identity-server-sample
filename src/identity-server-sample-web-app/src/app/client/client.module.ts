import { NgModule     } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SearchClientsComponent } from './components';

import { ClientRoutingModule } from './client-routing.module';

@NgModule({
  declarations: [
    SearchClientsComponent,
  ],
  imports: [
    CommonModule,
    ClientRoutingModule,
  ],
})
export class ClientModule { }
