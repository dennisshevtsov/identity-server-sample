import { ModuleWithProviders, NgModule     } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthorizationRoutingModule } from './authorization-routing.module';
import { userManagerProvider        } from './providers';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    AuthorizationRoutingModule,
  ],
})
export class AuthorizationModule {
  public static forRoot(): ModuleWithProviders<AuthorizationModule> {
    return {
      ngModule: AuthorizationModule,
      providers: [userManagerProvider],
    };
  }
}
