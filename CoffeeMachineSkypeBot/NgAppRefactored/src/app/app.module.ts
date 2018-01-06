import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule  } from '@angular/router';

const environment = {
  development: true,
  production: false,
};

import * as fromComponents from './components/';
import * as fromContainers from './containers/';
import * as fromSerices from './services';
import { Config } from './config';

// routes
export const ROUTES: Routes = [
  { path: '', component: [fromContainers.AppComponent] },
  { path: 'login', component: [fromContainers.LoginComponent] },
  { path: 'users', loadChildren: '../users/users.module#UsersModule', canActivate: [fromSerices.AuthGuard] },
  // otherwise redirect to home
	{ path: '**', redirectTo: '' }
];

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpModule,
    FormsModule,
    RouterModule.forRoot(ROUTES)
  ],
  declarations: [
    ...fromComponents.components,
    ...fromContainers.containers
  ],
  providers: [...fromSerices.services, Config],
  bootstrap: [fromContainers.AppComponent],
  entryComponents: [fromContainers.LoginComponent]
})

export class AppModule {
  private _host : string;
  private ConfigData : Object;

  constructor(private config: Config) {
		
		config.load()
		.then((value) => {
					this.ConfigData = this.config.getConfigData();
					this._host = this.config.getEnv('apiHostPath');
			}).catch((error) => console.error(error));
    }
}
