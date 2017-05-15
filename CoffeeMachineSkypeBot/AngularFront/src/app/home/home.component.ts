import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { User, PendingUser } from '../_models/index';
import { UserService,AlertService } from '../_services/index';

@Component({
	templateUrl: 'home.component.html'
})

export class HomeComponent implements OnInit 
{
	currentUser: User;
	pending: PendingUser [] = [];

	constructor(private route: ActivatedRoute,
				private router: Router,
				private userService: UserService,
				private alertService: AlertService)
	{
	}

	ngOnInit() {
		this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
		this.loadPendingUsers();
	}

	approveUser(id: number) {
		this.userService.approveUser(id).subscribe(() => { this.loadPendingUsers() });
	}

	approveAll(toApprove: PendingUser[]) {

		const ids = toApprove.map(function (user) { return user.id });
		//refresh UI
		this.userService.approveByIds(ids).subscribe(
							(data) => { this.loadPendingUsers(); },
							(error) => { this.alertService.error(error); });
	}

	private loadPendingUsers() {
		this.userService.getPendingUsers()
			.subscribe((users) => { 
							this.pending = users; 
							this.alertService.success('Users were loaded.');
									},
						(error)=>{ this.alertService.error(error); });
	}
}