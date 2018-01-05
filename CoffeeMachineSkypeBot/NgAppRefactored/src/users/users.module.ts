import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

// components
import * as fromComponents from './components';
// containers
import * as fromContainers from './containers';

// services
import * as fromServices from './services';
import { UserComponent } from './containers/users/user.component';

export const ROUTES: Routes = [
    {
      path: '',
      component: fromContainers.UserComponent,
    },
    {
      path: ':id',
      component: fromComponents.UserFormComponent,
    }
  ];

  @NgModule({
    imports: [
      CommonModule,
      ReactiveFormsModule,
      HttpClientModule,
      RouterModule.forChild(ROUTES),
    ],
    providers: [...fromServices.services],
    declarations: [...fromContainers.containers, ...fromComponents.components],
    exports: [...fromContainers.containers, ...fromComponents.components],
  })
  export class UsersModule {}