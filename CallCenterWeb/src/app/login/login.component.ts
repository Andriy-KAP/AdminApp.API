import { Component, Injectable } from '@angular/core';
import { UserModel } from './models/user.model';
import { LoginFormGroup } from './models/login-form.model';
import { LoginService } from './services/login.service';
import { ResponseModel } from "../common/models/response.model";
import { DataService } from "../common/services/data.service";
import { AppError } from "../common/models/app-error";
import { NotFoundError } from "../common/models/not-found-error";
import { Router } from "@angular/router";

@Component({
    selector: 'login',
    templateUrl: './login.component.html'
})

@Injectable()
export class LoginComponent{
    public userModel: UserModel;
    public formSubmitted: boolean = false;
    public form: LoginFormGroup;
    
    constructor(private loginService: LoginService, private router: Router){
        this.userModel = new UserModel();
        this.form = new LoginFormGroup();
    }
    private wrongCredFormError():void{
        this.form.setErrors({
            wrongCredentials: true
        });
    }
    private errorHandling(error?: AppError){
        if(error === null){
            this.wrongCredFormError();
            return;
        }
        if(error instanceof NotFoundError){
            this.wrongCredFormError();
            return;
        }
        else{
            throw error;
        }
    }
    login():void{
        let token: string;
        if(this.form.invalid){
            this.errorHandling(null);
            return;
        }
        this.loginService.getToken(this.form.value)
            .subscribe((data: ResponseModel)=>{
                if(data.data !=null){
                    DataService.saveToken(data.data);
                    this.router.navigate(['/']);
                }
                }, (error)=>{
                    this.errorHandling(error);
            });
    }
}