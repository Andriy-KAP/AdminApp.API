import { NgModule } from "@angular/core";
import { HomeComponent } from './home.component';
import { Routes ,RouterModule } from "@angular/router";
import { ChartModule } from "angular2-highcharts";
import { LoginComponent } from "./../login/login.component";
import { LayoutModule } from "../layout/layout.module";
import { AuthInterceptor } from "../common/interceptors/auth.interceptor";
import { HTTP_INTERCEPTORS } from "@angular/common/http";

declare var require: any;

const routes: Routes= [
    {
        path: '',
        component: HomeComponent
    }
];

@NgModule({
    declarations: [
        HomeComponent
    ],
    imports: [
        RouterModule.forChild(routes),
        ChartModule.forRoot(require('highcharts')),
        LayoutModule
    ],
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            multi: true
        }
    ],
    bootstrap: []
})
export class HomeModule{

}