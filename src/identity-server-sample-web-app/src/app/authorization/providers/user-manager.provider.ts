import { UserManager } from "oidc-client";

export const userManagerProvider = {
  provide: UserManager,
  useFactory: () => new UserManager({
    authority: 'http://localhost:5188',
    client_id: 'identity-server-sample-api-client-id-1',
    redirect_uri: 'http://localhost:4202/signin-callback',
    silent_redirect_uri: 'http://localhost:4202/silent-callback',
    post_logout_redirect_uri: 'http://localhost:4202',
    response_type: 'code',
    scope: 'openid profile identity-server-sample-api-scope',
  }),
};
