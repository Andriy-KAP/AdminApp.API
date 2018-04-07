import { FormControl, FormGroup, Validators } from '@angular/forms';

export class LoginFormControl extends FormControl{
    label: string;
    modelProperty: string;
    placeHolder: string;
    type: string;

    constructor(label:string, property:string, placeholder: string, type:string, value:any, validator: any){
        super(value, validator);
        this.label = label;
        this.modelProperty = property;
        this.placeHolder=placeholder;
        this.type = type;
    }

    getValidationMessages(){
        let messages:string[] = [];
        if(this.errors){
            for(let errorName in this.errors){
                switch(errorName){
                    case 'required' : messages.push(`${this.label} is required.`); break;
                    case 'minLength' : messages.push(`${this.label} must be at least ${this.errors['minLength'].requiredLength}`); break;
                }
            }
        }
        return messages;
    }
}

export class LoginFormGroup extends FormGroup{
    constructor(){
        super({
            username: new LoginFormControl("Username", "username", "Username", "text","", Validators.required),
            password: new LoginFormControl("Password", "password", "Password" , "password","", Validators.compose(
                [
                    Validators.required,
                    Validators.minLength(3)
                ]))
        });
    }

    get loginControls():LoginFormControl[]{
        return Object.keys(this.controls)
            .map(key=> this.controls[key] as LoginFormControl);
    }

    getFormValidationMessages(form: any) : string[]{
        let messages:string[]= [];
        this.loginControls.forEach(control=>control.getValidationMessages()
            .forEach(message=>messages.push(message)));
        return messages;
    }
}