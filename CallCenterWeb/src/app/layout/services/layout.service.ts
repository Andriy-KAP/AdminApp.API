import { HttpParams, HttpHeaders, HttpClient } from "@angular/common/http";
import { DataService } from "../../common/services/data.service";
import { Injectable } from "@angular/core";

@Injectable()
export class LayoutService extends DataService{
    constructor(http: HttpClient){
        super(http);
    }

    getLayoutInfo(){
    const headers = new HttpHeaders({
        'Content-Type': 'application/x-www-form-urlencoded',
        'Accept': 'application/json',
        'Authorization' : DataService.getToken()
    });

        return this.get('Info/GetInfo', headers);
    }
}