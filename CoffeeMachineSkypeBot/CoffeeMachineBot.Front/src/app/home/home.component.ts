import { Component, OnInit } from '@angular/core';

import { User, PendingUser } from '../_models/index';
import { UserService } from '../_services/index';

@Component({
	//moduleId: module.id,
	templateUrl: 'home.component.html'
})

export class HomeComponent implements OnInit {

	currentUser: User;
	pending: PendingUser [] = [];

	constructor(private userService: UserService) {
		this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
	}

	ngOnInit() {
		this.loadPendingUsers();
	}

	approveUser(id: number) {
		this.userService.approveUser(id).subscribe(() => { this.loadPendingUsers() });
	}

	approveAll(toApprove: PendingUser[]) {

		let ids = toApprove.map(function (el) { return el.id });

		this.userService.approveByIds(ids).subscribe(() => { this.loadPendingUsers() });
	}

	private loadPendingUsers() {
		this.userService.getPendingUsers().subscribe(users => { this.pending = users; });
	}
}