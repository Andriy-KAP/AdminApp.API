import { UserModel } from "../models/user.model";
import { Injectable } from "@angular/core";
import { HttpParams, HttpHeaders, HttpClient } from "@angular/common/http";
import { DataService } from "../../common/services/data.service";
import { Observable } from 'rxjs/Observable';
import { AppError } from "../../common/models/app-error";
import { NotFoundError } from "../../common/models/not-found-error";
import { BadInput } from "../../common/models/bad-input";
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class LoginService extends DataService{
    constructor(http: HttpClient){
        super(http);
    }
   

    getToken(user: UserModel){
        
        const params=new HttpParams()
            .set('Email', user.username)
            .set('Password', user.password);

        const headers= new HttpHeaders({
            'Content-Type': 'application/x-www-form-urlencoded',
            'Accept': 'application/json'
        });

        return  this.post('Account/Login', headers, params);
    }
}