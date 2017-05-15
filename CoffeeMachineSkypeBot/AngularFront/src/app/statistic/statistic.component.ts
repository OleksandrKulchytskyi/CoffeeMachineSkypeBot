import { Component, ViewChild, ElementRef, EventEmitter } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { StatisticService, AlertService } from '../_services/index';

@Component({
	templateUrl: 'statistic.component.html'
})

export class StatisticComponent
{
	loading : boolean;
	@ViewChild('fileInput') myFileInput: ElementRef;

	constructor(private statisticService: StatisticService,
				private alertService: AlertService)	
	{ 
		this.loading = false;
	}

	onFileChange(event: any) {
	
		const fileList: FileList = event.target.files;
		if(!fileList || fileList.length <= 0){
			this.alertService.error('Please select a file for further upload.');
		}

		this.loading = true;
		const file: File = fileList[0];

		this.statisticService.submitStatistic(file)
			.subscribe((logErrors) => { 
									console.log(logErrors);
									this.alertService.success(logErrors);
									this.loading = false;
									},
						(error) => {
									this.loading = false;
						 			this.alertService.error(error); });
	}
}