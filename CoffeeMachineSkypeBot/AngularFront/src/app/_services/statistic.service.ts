//import { Injectable, ViewChild, ElementRef} from '@angular/core';
import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

@Injectable()
export class StatisticService {

	constructor(private http: Http) { }

	fileChange(event: any) {

		const fileList: FileList = event.target.files;

		console.log(fileList);

		if (fileList.length > 0) {

			const file: File = fileList[0];
			const formData: FormData = new FormData();
			formData.append('uploadFile', file, file.name);
			const headers = new Headers();
			headers.append('Content-Type', 'multipart/form-data');
			headers.append('Accept', 'application/json');
			const options = new RequestOptions({ headers: headers });
			this.http.post('/api/statistics/upload', formData, options)
					.map(res => res.json())
					.catch(error => Observable.throw(error))
					.subscribe( data => console.log('success'),
								error => console.log(error))
		}
	}
}