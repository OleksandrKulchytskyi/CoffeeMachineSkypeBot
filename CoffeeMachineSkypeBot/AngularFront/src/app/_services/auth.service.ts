import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { User } from '../_models/index';
import { Config } from '../config';

@Injectable()
export class AuthService {

	private  _configuration: Config;
	storageItem = "currentUser";

	constructor(private http: Http, private config: Config) { 
		this._configuration = config;
	}

	isAuthenticated()
	{
		if (!localStorage.getItem(this.storageItem) ||
			localStorage.getItem(this.storageItem) == "") {
			return false;
		} 
		else {
			return true;
		}
	}

	getUserName()
	{
		if (!localStorage.getItem(this.storageItem) ||
			localStorage.getItem(this.storageItem) == "") {
			return "Unknown";
		} 
		else {
			const usr: User = JSON.parse(localStorage.getItem(this.storageItem));
			return usr.username;
		}
	}

	login(username: string, password: string) {

		const body = JSON.stringify({ UserName: username, Password: password });
		const headers = new Headers({ 'Content-Type': 'application/json' });
		const options = new RequestOptions({ headers: headers });
		
		let apiPath = '/api/auth';
		const data = this._configuration.getConfigData();

		if(data){
			apiPath = data.apiHostPath + apiPath;
		}

		return this.http.post(apiPath, body, options)
						.map((response: Response) => {
							// login successful if there's a jwt token in the response
							const user = response.json();
							if (user && user.token) {
								// store user details and jwt token in local storage to keep user logged in between page refreshes
								localStorage.setItem(this.storageItem, JSON.stringify(user));
							}
						});
	}

	logout() {
		// remove user from local storage to log user out
		localStorage.removeItem(this.storageItem);
	}
}