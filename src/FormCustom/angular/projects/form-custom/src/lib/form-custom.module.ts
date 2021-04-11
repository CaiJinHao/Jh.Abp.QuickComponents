import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { FormCustomComponent } from './components/form-custom.component';
import { FormCustomRoutingModule } from './form-custom-routing.module';

@NgModule({
  declarations: [FormCustomComponent],
  imports: [CoreModule, ThemeSharedModule, FormCustomRoutingModule],
  exports: [FormCustomComponent],
})
export class FormCustomModule {
  static forChild(): ModuleWithProviders<FormCustomModule> {
    return {
      ngModule: FormCustomModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<FormCustomModule> {
    return new LazyModuleFactory(FormCustomModule.forChild());
  }
}
