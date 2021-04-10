import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'MenuManagement',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44313',
    redirectUri: baseUrl,
    clientId: 'MenuManagement_App',
    responseType: 'code',
    scope: 'offline_access MenuManagement role email openid profile',
  },
  apis: {
    default: {
      url: 'https://localhost:44313',
      rootNamespace: 'Jh.Abp.MenuManagement',
    },
    MenuManagement: {
      url: 'https://localhost:44332',
      rootNamespace: 'Jh.Abp.MenuManagement',
    },
  },
} as Environment;
