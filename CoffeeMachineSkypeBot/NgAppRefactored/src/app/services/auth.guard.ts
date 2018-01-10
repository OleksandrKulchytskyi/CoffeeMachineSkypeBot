import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable()
export class AuthGuard implements CanActivate {

	constructor(private router: Router) { }

	canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
		if (localStorage.getItem('currentUser')) {
			// user is logged-in, so return true
			return true;
		}

		// User isn't logged-in, so redirect to login page with the return url
		this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
		return false;
	}
}