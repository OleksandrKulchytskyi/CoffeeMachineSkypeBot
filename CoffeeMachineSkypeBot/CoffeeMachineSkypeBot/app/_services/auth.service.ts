import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'

@Injectable()
export class AuthService {
	constructor(private http: Http) { }

	login(username: string, password: string) {

		let body = JSON.stringify({ UserName: username, Password: password });
		let headers = new Headers({ 'Content-Type': 'application/json' });
		let options = new RequestOptions({ headers: headers });

		return this.http.post('/api/auth', body, options)
			.map((response: Response) => {
				// login successful if there's a jwt token in the response
				let user = response.json();
				if (user && user.token) {
					// store user details and jwt token in local storage to keep user logged in between page refreshes
					localStorage.setItem('currentUser', JSON.stringify(user));
				}
			});
	}

	logout() {
		// remove user from local storage to log user out
		localStorage.removeItem('currentUser');
	}
}