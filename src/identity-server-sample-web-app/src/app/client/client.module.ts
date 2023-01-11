import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddClientComponent } from './components/add-client/add-client.component';
import { UpdateClientComponent } from './components/update-client/update-client.component';
import { SearchClientsComponent } from './components/search-clients/search-clients.component';
import { ClientComponent } from './components/client/client.component';



@NgModule({
  declarations: [
    AddClientComponent,
    UpdateClientComponent,
    SearchClientsComponent,
    ClientComponent
  ],
  imports: [
    CommonModule
  ]
})
export class ClientModule { }
