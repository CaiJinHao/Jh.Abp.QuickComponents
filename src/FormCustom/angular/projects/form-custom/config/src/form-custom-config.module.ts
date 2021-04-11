import { ModuleWithProviders, NgModule } from '@angular/core';
import { FORM_CUSTOM_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class FormCustomConfigModule {
  static forRoot(): ModuleWithProviders<FormCustomConfigModule> {
    return {
      ngModule: FormCustomConfigModule,
      providers: [FORM_CUSTOM_ROUTE_PROVIDERS],
    };
  }
}
