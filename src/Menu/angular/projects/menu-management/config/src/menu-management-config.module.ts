import { ModuleWithProviders, NgModule } from '@angular/core';
import { MENU_MANAGEMENT_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class MenuManagementConfigModule {
  static forRoot(): ModuleWithProviders<MenuManagementConfigModule> {
    return {
      ngModule: MenuManagementConfigModule,
      providers: [MENU_MANAGEMENT_ROUTE_PROVIDERS],
    };
  }
}
