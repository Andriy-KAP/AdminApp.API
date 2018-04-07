import { DataService } from "../../common/services/data.service";
import { HttpParams, HttpHeaders, HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable()
export class UserService {
    // constructor(http: HttpClient){
    //     super(http);
    // }
    constructor(private dataService: DataService){

    }

    getUsers(pageIndex: number, pageSize: number){
        const params = new HttpParams()
            .set('pageIndex', pageIndex.toString())
            .set('pageSize', pageSize.toString())
        const headers= new HttpHeaders({
            'Authorization': DataService.getToken()
        });
        
        return this.dataService.get(`User/GetUserCollection?PageIndex=${pageIndex}&PageSize=${pageSize}`, null)//, headers);
    }
}