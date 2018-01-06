import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'Coffee Machine Ng app';
  authService: AuthService;
	loggedUserName: string;

constructor(private secureService: AuthService,
              private route: ActivatedRoute,
              private router: Router) {
    this.authService = secureService;
		this.loggedUserName = '';
}

ngOnInit() {
if (!this.authService.isAuthenticated()) {
    this.router.navigate(['/login']);
}
else {
    this.loggedUserName = this.authService.getUserName();
  }	
}

logout(): void {
if (this.authService.isAuthenticated()) {
    this.authService.logout();
  }
}

}
