import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { User, PendingUser } from '../../models/';
import { UserService } from '../../services/';

@Component({
	templateUrl: 'user.component.html'
})

export class UserComponent implements OnInit {
	currentUser: User;
	pending: PendingUser[] = [];

    // private alertService: AlertService
	constructor(private route: ActivatedRoute,
				private router: Router,
				private userService: UserService) {
	}

	ngOnInit() {
		this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
		this.loadPendingUsers();
	}

	approveUser(id: number) {
		this.userService.approveUser(id).subscribe(() => { this.loadPendingUsers(); });
	}

	approveAll(toApprove: PendingUser[]) {

		const ids = toApprove.map(function (user) { return user.id });
		// refresh UI
		this.userService.approveByIds(ids).subscribe(
							(data) => { this.loadPendingUsers(); },
							(error) => { console.log(error); }); // this.alertService.error(error); });
	}

	private loadPendingUsers() {
		this.userService.getPendingUsers()
			.subscribe((users) => {
							this.pending = users;
                            // this.alertService.success('Users were loaded.');
                            console.info('Users were loaded.');
									},
						(error) => { console.error('Users were loaded.'); }); // this.alertService.error(error); 
	}
}