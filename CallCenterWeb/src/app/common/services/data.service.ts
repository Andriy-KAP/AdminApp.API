import { HttpClient, HttpParams, HttpHeaders } from "@angular/common/http";
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { Observable } from "rxjs/Observable";
import { NotFoundError } from "../models/not-found-error";
import { BadInput } from "../models/bad-input";
import { AppError } from "../models/app-error";
import { Injectable } from "@angular/core";

@Injectable()
export class DataService {
    public static serverUrl:string='http://localhost:55484/api/';

    constructor(private http: HttpClient){
        
    }
    static saveToken(token: string):void{
        sessionStorage.setItem('auth', token);
    }
    static getToken():string{
        return sessionStorage.getItem('auth');
    }
    static removeToken():void{
        sessionStorage.removeItem('auth');
    }

    private errorHandling(error:Response){
        if(error.status === 404)
            return Observable.throw(new NotFoundError());
        if(error.status === 400)
            return Observable.throw(new BadInput());
        return Observable.throw(new AppError(error));
    }

    public get(url:string, headers: HttpHeaders){
        return this.http.get(DataService.serverUrl.concat(url), { headers })
            .catch(this.errorHandling);
    }
    public post(url:string, headers: HttpHeaders, params?: HttpParams){
        return this.http.post(DataService.serverUrl.concat(url), params, { headers })
            .catch(this.errorHandling);
    }
}