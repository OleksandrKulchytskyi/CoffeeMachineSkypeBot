import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';

export interface ApiConfig 
{
    apiHostPath: string;
    forceLogin: boolean;
}

@Injectable()
 export class Config 
 {

 private _env: Object;
 private _path: string;
 private apiConfig: ApiConfig;
 private isLoaded: boolean;

 constructor(private http: Http)
 {
     //this._path = 'config/env.debug.json';
     this._path = 'config/env.json';
     this.isLoaded = false;
 }
 
 load() {
     
    return new Promise((resolve, reject) => {
   
        this.http.get(this._path)
                 .map(res => res.json())
                 .subscribe(
                     (data) => {
                        this.isLoaded = true;
                        this._env = data;        
                        this.apiConfig = data;
                        resolve(data);
                    },
                    error => {
                        this.isLoaded = false;
                        reject(error);
                    });
   });
}
 
 getEnv(key: any) {
     if(!this.isLoaded){
         this.load();
     }

   return this._env[key];
 }
 
 getConfigData(){
     if(!this.isLoaded){
         this.load();
     }

     return this.apiConfig;
 }
};