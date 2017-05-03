import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/index';
import { LoginComponent } from './login/index';
import { StatisticComponent } from './statistic/index';
import { AuthGuard } from './_auth/index';

const appRoutes: Routes = [
	//{ path: '', component: App }
	{ path: 'login', component: LoginComponent },
	{ path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
	{ path: 'statistics', component: StatisticComponent, canActivate: [AuthGuard] },

	// otherwise redirect to home
	{ path: '**', redirectTo: '' }
];

export const routing = RouterModule.forRoot(appRoutes);