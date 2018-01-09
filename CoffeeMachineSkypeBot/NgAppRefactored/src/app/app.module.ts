import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { LoginModule } from '../login/login.module';


const environment = {
  development: true,
  production: false,
};

import * as fromSerices from './services';
import { Config } from './core/config';
import { AppComponent } from './app.component';
import { PageNotFoundComponent } from './core/containers/page-not-found/page-not-found.component';

// routes
export const ROUTES: Routes = [
  { path: '', redirectTo: 'users', pathMatch: 'full'},
  { path: 'page-not-found', component: PageNotFoundComponent },
  { path: 'login', loadChildren: '../login/login.module#LoginModule' },
  { path: 'users', loadChildren: '../users/users.module#UsersModule', canActivate: [fromSerices.AuthGuard] },
  // otherwise redirect to home
	{ path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpModule,
    FormsModule,
    CoreModule,
    SharedModule,
    LoginModule,
    RouterModule.forRoot(ROUTES)
  ],
  declarations: [ AppComponent ],
  providers: [...fromSerices.services, Config],
  exports: [RouterModule],
  bootstrap: [AppComponent],
})

export class AppModule {

private host: string;
private configData: Object;

constructor(private config: Config) {
		config.load()
		.then((value) => {
					this.configData = this.config.getConfigData();
					this.host = this.config.getEnv('apiHostPath');
			}).catch((error) => console.error(error));
}
}
