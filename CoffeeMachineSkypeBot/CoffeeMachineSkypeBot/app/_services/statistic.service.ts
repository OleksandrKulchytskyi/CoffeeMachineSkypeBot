//import { Injectable, ViewChild, ElementRef} from '@angular/core';
import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'

@Injectable()
export class StatisticService {

	constructor(private http: Http) { }

	fileChange(event: any) {

		let fileList: FileList = event.target.files;

		if (fileList.length > 0) {

			let file: File = fileList[0];
			let formData: FormData = new FormData();
			formData.append('uploadFile', file, file.name);
			let headers = new Headers();
			headers.append('Content-Type', 'multipart/form-data');
			headers.append('Accept', 'application/json');
			let options = new RequestOptions({ headers: headers });
			this.http.post('/api/statistics/upload', formData, options)
					.map(res => res.json())
					.catch(error => Observable.throw(error))
					.subscribe( data => console.log('success'),
								error => console.log(error))
		}
	}
}