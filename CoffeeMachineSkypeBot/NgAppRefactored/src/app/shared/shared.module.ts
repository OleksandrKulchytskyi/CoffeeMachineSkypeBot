import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import * as fromComponents from './components';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [...fromComponents.components],
  exports: [...fromComponents.components],
})
export class SharedModule { }
