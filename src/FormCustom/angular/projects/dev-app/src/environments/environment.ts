import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'FormCustom',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44359',
    redirectUri: baseUrl,
    clientId: 'FormCustom_App',
    responseType: 'code',
    scope: 'offline_access FormCustom role email openid profile',
  },
  apis: {
    default: {
      url: 'https://localhost:44359',
      rootNamespace: 'Jh.Abp.FormCustom',
    },
    FormCustom: {
      url: 'https://localhost:44378',
      rootNamespace: 'Jh.Abp.FormCustom',
    },
  },
} as Environment;
