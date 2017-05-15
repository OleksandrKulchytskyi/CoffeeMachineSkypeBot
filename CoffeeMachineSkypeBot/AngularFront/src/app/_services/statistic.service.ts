import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { Config } from '../config';

@Injectable()
export class StatisticService {

	constructor(private http: Http, 
				private config: Config) 
	{ 
	}

	submitStatistic(file: File) {
			
		const formData: FormData = new FormData();
		formData.append('uploadFile', file, file.name);
			
		const headers = new Headers();
		headers.append('Accept', 'application/json');
		headers.append('Content-Type', 'multipart/form-data');

		const currentUser = JSON.parse(localStorage.getItem('currentUser'));
		if (currentUser && currentUser.token) {
			headers.append('Authorization', 'Bearer ' + currentUser.token);
		}
			
		const options = new RequestOptions({ headers: headers });
		
		return this.http.post(this.config.getEnv('apiHostPath') + '/api/StatisticsApi/uploadsinglefile', formData, options)
						.map(res => res.json());
	}
}