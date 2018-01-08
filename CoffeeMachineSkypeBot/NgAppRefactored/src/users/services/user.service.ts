import { Injectable } from '@angular/core';
import { Http,  Headers,  RequestOptions, Response} from '@angular/http';
import {  User,  PendingUser } from '../models';
import 'rxjs/add/operator/map';
import {  Config } from '../../app/core/config';

@Injectable()
export class UserService {

  private apiPath: String;

  constructor(private http: Http, private config: Config) {
    this.apiPath = this.config.getConfigData().apiHostPath;
  }

  getPendingUsers() {
    return this.http.get(this.apiPath + '/api/pending/getall', this.includeJWT())
      .map((resp: Response) => resp.json());
  }

  approveUser(id: number) {
    return this.http.put(this.apiPath + '/api/pending/single/' + id, this.includeJWT())
      .map((resp: Response) => resp.json());
  }

  approveByIds(ids: number[]) {
    return this.http.post(this.apiPath + '/api/pending/byids', ids, this.includeJWT())
      .map((resp: Response) => resp.json());
  }

  getAll() {
    return this.http.get(this.apiPath + '/api/users', this.includeJWT())
      .map((response: Response) => response.json());
  }

  getById(id: number) {
    return this.http.get(this.apiPath + '/api/users/' + id, this.includeJWT())
      .map((response: Response) => response.json());
  }

  create(user: User) {
    return this.http.post(this.apiPath + '/api/users', user, this.includeJWT())
      .map((response: Response) => response.json());
  }

  update(user: User) {
    return this.http.put(this.apiPath + '/api/users/' + user.id, user, this.includeJWT())
      .map((response: Response) => response.json());
  }

  delete(id: number) {
    return this.http.delete(this.apiPath + '/api/users/' + id, this.includeJWT())
      .map((response: Response) => response.json());
  }

  // private helper methods
  private includeJWT() {
    // create authorization header with includeJWT token
    const currentUser = JSON.parse(localStorage.getItem('currentUser'));
    if (currentUser && currentUser.token) {
      const headers = new Headers({
        'Authorization': 'Bearer ' + currentUser.token
      });
      return new RequestOptions({
        headers: headers
      });
    }
  }
}
