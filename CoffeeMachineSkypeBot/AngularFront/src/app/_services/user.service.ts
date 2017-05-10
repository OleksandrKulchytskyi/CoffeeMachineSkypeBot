import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { User, PendingUser } from '../_models/index';

@Injectable()
export class UserService {

	constructor(private http: Http) { }

	getPendingUsers() {
		return this.http.get('api/pending/getall', this.includeJWT()).map((resp: Response) => resp.json());
	}

	approveUser(id: number) {
		return this.http.put('/api/pending/single/' + id, this.includeJWT()).map((resp: Response) => resp.json());
	}

	approveByIds(ids: number[]) {
		return this.http.post('/api/pending/byids', ids, this.includeJWT()).map((resp: Response) => resp.json());
	}

	getAll() {
		return this.http.get('/api/users', this.includeJWT()).map((response: Response) => response.json());
	}

	getById(id: number) {
		return this.http.get('/api/users/' + id, this.includeJWT()).map((response: Response) => response.json());
	}

	create(user: User) {
		return this.http.post('/api/users', user, this.includeJWT()).map((response: Response) => response.json());
	}

	update(user: User) {
		return this.http.put('/api/users/' + user.id, user, this.includeJWT()).map((response: Response) => response.json());
	}

	delete(id: number) {
		return this.http.delete('/api/users/' + id, this.includeJWT()).map((response: Response) => response.json());
	}

	// private helper methods

	private includeJWT() {
		// create authorization header with includeJWT token
		const currentUser = JSON.parse(localStorage.getItem('currentUser'));
		if (currentUser && currentUser.token) {
			const headers = new Headers({ 'Authorization': 'Bearer ' + currentUser.token });
			return new RequestOptions({ headers: headers });
		}
	}
}