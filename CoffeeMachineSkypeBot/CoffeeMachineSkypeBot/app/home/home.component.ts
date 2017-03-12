import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { User, PendingUser } from '../_models/index';
import { UserService } from '../_services/index';

@Component({
	moduleId: module.id,
	templateUrl: 'home.component.html'
})

export class HomeComponent implements OnInit {

	currentUser: User;
	pending: PendingUser [] = [];

	constructor(private route: ActivatedRoute,
				private router: Router,
				private userService: UserService)
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

		let ids = toApprove.map(function (el) { return el.id });

		this.userService.approveByIds(ids).subscribe(() => { this.loadPendingUsers() });
	}

	private loadPendingUsers() {
		this.userService.getPendingUsers().subscribe(users => { this.pending = users; });
	}
}