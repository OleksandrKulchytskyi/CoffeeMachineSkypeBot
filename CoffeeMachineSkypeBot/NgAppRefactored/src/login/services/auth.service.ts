import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { User } from '../../users/models/user';
import { Config } from '../../app/core/config';

@Injectable()
export class AuthService {

private  configuration: Config;
private storageItem = 'currentUser';

constructor(private http: Http, private config: Config) {
	this.configuration = config;
}

isAuthenticated(): boolean {
	if (!localStorage.getItem(this.storageItem) ||
		localStorage.getItem(this.storageItem) === '') {
		return false;
	} 
	else {
		return true;
	}
}

getUserName(): string {
if (!localStorage.getItem(this.storageItem) || localStorage.getItem(this.storageItem) === '') {
    return 'Unknown';
}
else {
	const user: User = JSON.parse(localStorage.getItem(this.storageItem));
	return user.username;
}
}

login(username: string, password: string) {

const body = JSON.stringify({ UserName: username, Password: password });
const headers = new Headers({ 'Content-Type': 'application/json' });
const options = new RequestOptions({ headers: headers });

let apiPath = '/api/auth';
const data = this.configuration.getConfigData();

if (data) {
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

logout(): void {
// remove user from local storage to log user out
localStorage.removeItem(this.storageItem);
}

}
