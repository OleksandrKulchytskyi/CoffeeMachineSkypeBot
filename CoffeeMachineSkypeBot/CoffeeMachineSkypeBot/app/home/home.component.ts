import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { User, PendingUser } from '../_models/index';
import { UserService } from '../_services/index';

@Component({
	moduleId: module.id,
	templateUrl: 'home.component.html'
})

export class HomeComponent implements OnInit {

	returnUrl: string;
	currentUser: User;
	pending: PendingUser [] = [];

	constructor(private route: ActivatedRoute,
				private router: Router,
				private userService: UserService) {
		this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
	}

	ngOnInit() {
		this.loadPendingUsers();
		this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/statistics';
	}

	approveUser(id: number) {
		this.userService.approveUser(id).subscribe(() => { this.loadPendingUsers() });
	}

	approveAll(toApprove: PendingUser[]) {

		let ids = toApprove.map(function (el) { return el.id });

		this.userService.approveByIds(ids).subscribe(() => { this.loadPendingUsers() });
	}

	navigateToStatistics() {
		this.router.navigate([this.returnUrl]);
	}

	private loadPendingUsers() {
		this.userService.getPendingUsers().subscribe(users => { this.pending = users; });
	}
}