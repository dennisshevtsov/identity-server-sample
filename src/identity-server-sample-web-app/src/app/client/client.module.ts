import { NgModule            } from '@angular/core';
import { CommonModule        } from '@angular/common';
import { HttpClientModule    } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

import { AddClientComponent     } from './components';
import { ClientComponent        } from './components';
import { SearchClientsComponent } from './components';
import { UpdateClientComponent  } from './components';

import { HTTP_INTERCEPTOR_PROVIDER } from './interceptors';

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
    HttpClientModule,
    ReactiveFormsModule,
    ClientRoutingModule,
  ],
  providers: [HTTP_INTERCEPTOR_PROVIDER],
})
export class ClientModule { }
