import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { MenuManagementComponent } from './components/menu-management.component';
import { MenuManagementRoutingModule } from './menu-management-routing.module';

@NgModule({
  declarations: [MenuManagementComponent],
  imports: [CoreModule, ThemeSharedModule, MenuManagementRoutingModule],
  exports: [MenuManagementComponent],
})
export class MenuManagementModule {
  static forChild(): ModuleWithProviders<MenuManagementModule> {
    return {
      ngModule: MenuManagementModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<MenuManagementModule> {
    return new LazyModuleFactory(MenuManagementModule.forChild());
  }
}
