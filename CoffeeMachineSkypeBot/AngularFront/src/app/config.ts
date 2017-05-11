import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';

export class ApiConfig{
    apiHostPath: string;
    forceLogin:boolean;
}

@Injectable()
 export class Config {

 private _config: Object;
 private _env: Object;
 private _path: string;
 private apiConfig: ApiConfig;

 constructor(private http: Http) {
     this._path='config/env.json';
 }
 
 load() {
 return new Promise((resolve, reject) => {
   this.http.get(this._path)
        .map(res => res.json())
        .subscribe((env_data) => {
            this._env = env_data;        
            this.apiConfig = env_data;
            resolve(env_data);
     });
   });
}
 
 getEnv(key: any) {
   return this._env[key];
 }
 
 get(key: any) { 
   return this._config[key];
 }

 getConfigData(){
     return this.apiConfig;
 }
};