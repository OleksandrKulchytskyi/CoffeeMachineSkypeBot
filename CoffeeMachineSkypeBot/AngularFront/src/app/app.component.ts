import { Component , OnInit} from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from './_services/index';

@Component({
	selector: 'app',
	templateUrl: 'app.component.html'
})

export class AppComponent implements OnInit {

	authService: AuthService;
	loggedUserName: string;

	constructor(private secureService: AuthService,
				private route: ActivatedRoute,
				private router: Router)
	{
		this.authService = secureService;
		this.loggedUserName = '';
	}

	ngOnInit() {
		if(!this.authService.isAuthenticated()){
			this.router.navigate(["/login"]);
		}
		else {
			this.loggedUserName = this.authService.getUserName();
		}	
	}

	logout(){
		if(this.authService.isAuthenticated()){
			this.authService.logout();
		}
	}
}