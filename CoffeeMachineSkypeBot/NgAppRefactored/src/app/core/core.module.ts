import {ModuleWithProviders, NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';

import * as fromServices from './services';
import * as fromComponents from './components';
// Containers
import { PageNotFoundComponent } from './containers/page-not-found/page-not-found.component';

import { Config } from './config';

@NgModule({
  imports: [ CommonModule ],
  declarations: [...fromComponents.components, PageNotFoundComponent],
  exports: [...fromComponents.components, PageNotFoundComponent],
  providers: [...fromServices.services, Config]
})

export class CoreModule {

constructor (@Optional() @SkipSelf() parentModule: CoreModule) {
    if (parentModule) {
      throw new Error('CoreModule is already loaded. Import it in the AppModule only.');
    }
}

static forRoot(): ModuleWithProviders {
   return {
      ngModule: CoreModule,
      providers: [ ]
      // {provide: UserServiceConfig, useValue: config }
    };
}

}
