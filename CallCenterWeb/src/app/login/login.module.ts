import { LoginComponent } from "./login.component";
import { Routes, RouterModule } from "@angular/router";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from "@angular/common";
import { LoginService } from "./services/login.service";
import { HttpClientModule } from '@angular/common/http';
import { SetClassDirective } from "./attributes/attr.setClass";

const routes: Routes=[
    {
        path: '',
        component: LoginComponent
    }
]

@NgModule({
    declarations: [
        LoginComponent,
        SetClassDirective
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forChild(routes),
        HttpClientModule
    ],
    providers: [
        LoginService
    ],
    bootstrap: []
})
export class LoginModule{

}