import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { ClientInterceptor } from './client.interceptor';

export const HTTP_INTERCEPTOR_PROVIDER = [
  {
    provide : HTTP_INTERCEPTORS,
    useClass: ClientInterceptor,
    multi   : true,
  },
];
