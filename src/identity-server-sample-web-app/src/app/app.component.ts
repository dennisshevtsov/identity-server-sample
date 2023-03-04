import { Component } from '@angular/core';

import { UserManager } from 'oidc-client';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent {
  public constructor(private readonly userManager: UserManager) { }

  public signout(): void {
    this.userManager.signoutRedirect();
  }
}
