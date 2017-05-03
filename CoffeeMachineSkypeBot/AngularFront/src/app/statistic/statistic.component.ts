import { Component, ViewChild, ElementRef, EventEmitter } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { StatisticService } from '../_services/index';

@Component({
	//moduleId: module.id,
	templateUrl: 'statistic.component.html'
})

export class StatisticComponent
{
	loaded: boolean;
	@ViewChild('fileInput') myFileInput: ElementRef;

	constructor(private statisticService: StatisticService)
				//,private emitter: EventEmitter<any>)
	{
	}

	onChange(event: any) {

		this.loaded = false;

		console.log(event);
		this.statisticService.fileChange(event);

		this.loaded = true;
	}
}