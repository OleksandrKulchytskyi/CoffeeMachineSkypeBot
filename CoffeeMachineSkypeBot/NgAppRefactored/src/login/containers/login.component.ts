import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../services/';
import { AlertService } from '../../app/core/services/alert.service';

@Component({
	templateUrl: 'login.component.html'
})

export class LoginComponent implements OnInit {

loading = false;
returnUrl: string;
model: any = { username: '' , password: '' };

constructor(private route: ActivatedRoute,
			private router: Router,
			private authenticationService: AuthService,
			private alertService: AlertService) { }

ngOnInit() {
	// reset login status
	this.authenticationService.logout();
	// get return url from route parameters or default to '/'
	this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
}

login(): void {
	this.loading = true;
	this.authenticationService.login(this.model.username, this.model.password)
		.subscribe((data) => { this.router.navigate([this.returnUrl]); },
					(error) => {
				this.alertService.error(error);
				this.loading = false;
			});
}

logout() {
	this.authenticationService.logout();
}

}
