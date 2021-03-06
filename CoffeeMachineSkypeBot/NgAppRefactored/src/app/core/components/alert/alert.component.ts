import { Component, OnInit } from '@angular/core';

import { AlertService } from '../../services/alert.service';

@Component({
	selector: 'alert-notification',
	templateUrl: 'alert.component.html'
})

export class AlertComponent implements OnInit  {
    message: any;
	constructor(private alertService: AlertService) { }

	ngOnInit() {
        this.alertService.getMessage().subscribe(message => { this.message = message; });
	}
}