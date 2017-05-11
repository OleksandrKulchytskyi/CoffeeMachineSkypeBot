import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

// used to create fake backend
//import { fakeBackendProvider } from './_fakes/index';
//import { MockBackend, MockConnection } from '@angular/http/testing';
import { BaseRequestOptions } from '@angular/http';

import { AppComponent } from './app.component';
import { Config } from './config';
import { routing } from './app.routing';

import { AlertComponent } from './_alerts/index';
import { AuthGuard } from './_auth/index';
import { AlertService, AuthService, UserService, StatisticService } from './_services/index';
import { HomeComponent } from './home/index';
import { LoginComponent } from './login/index';
import { StatisticComponent } from './statistic/index';

@NgModule({
	imports: [
		BrowserModule,
		FormsModule,
		HttpModule,
		routing
	],
	declarations: [
		AppComponent,
		AlertComponent,
		HomeComponent,
		LoginComponent,
		StatisticComponent
	],
	providers: [
		AuthGuard,
		AlertService,
		AuthService,
		UserService,
		StatisticService,
		BaseRequestOptions,
		Config
		//fakeBackendProvider,
		//MockBackend,
	],
	bootstrap: [AppComponent]
})

export class AppModule {
  
  private _host : string;
  private ConfigData : Object;

  constructor(private config : Config) {

		config.load().then((value) => {
			this.ConfigData =  config.getConfigData();
			this._host=this.config.getEnv("apiHostPath");
			console.log(this._host);
		});
    }
}