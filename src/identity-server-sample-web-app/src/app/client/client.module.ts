import { NgModule            } from '@angular/core';
import { CommonModule        } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { AddClientComponent     } from './components';
import { ClientComponent        } from './components';
import { SearchClientsComponent } from './components';

import { ClientRoutingModule } from './client-routing.module';

@NgModule({
  declarations: [
    AddClientComponent,
    ClientComponent,
    SearchClientsComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ClientRoutingModule,
  ],
})
export class ClientModule { }
