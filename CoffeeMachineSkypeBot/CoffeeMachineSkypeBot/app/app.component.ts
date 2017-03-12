import { Component , OnInit} from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from './_services/index';

@Component({
	moduleId: module.id,
	selector: 'app',
	templateUrl: 'app.component.html'
})

export class AppComponent implements OnInit {

	authService: AuthService;

	constructor(private secureService: AuthService,
				private route: ActivatedRoute,
				private router: Router,)
	{
		this.authService = secureService;
	}

	ngOnInit() {
		if(!this.authService.isAuthenticated()){
			this.router.navigate(["/login"]);
		}
	}
}