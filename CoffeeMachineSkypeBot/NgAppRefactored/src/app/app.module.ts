import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { Routes, RouterModule  } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

const environment = {
  development: true,
  production: false,
};

import { AppComponent } from './containers/app.component';
import * as fromComponents from './components/';
import * as fromSerices from './services';
import { Config } from './config';

// routes
export const ROUTES: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'users' },
  {
    path: 'users',
    loadChildren: '../users/users.module#UsersModule',
  }
];

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(ROUTES)
  ],
  declarations: [
    ...fromComponents.components,
    AppComponent
  ],
  providers: [...fromSerices.services, Config],
  bootstrap: [AppComponent]
})
export class AppModule { }
