import { Component, ViewChild, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { StatisticService } from '../_services/index';

@Component({
	moduleId: module.id,
	templateUrl: 'statistic.component.html'
})


export class StatisticComponent
{
	loaded: boolean;
	@ViewChild('fileInput') myFileInput: ElementRef;

	constructor( private statisticService: StatisticService)
	{
	}

	uploadFile(event: any) {
		this.loaded = false;

		this.statisticService.fileChange(event);
		this.loaded = true;
	}
}