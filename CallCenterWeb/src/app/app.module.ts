import { NgModule, OnInit, ErrorHandler } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { NavigationState } from "./app.service";
import { ChartModule } from 'angular2-highcharts';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { HttpModule } from '@angular/http';
import { AppErrorHandler } from "./common/services/app-error-handler";
import { LayoutComponent } from "./layout/layout.component";
import { LoginComponent } from "./login/login.component";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginService } from "./login/services/login.service";
import { SetClassDirective } from "./login/attributes/attr.setClass";
import { CanActivateUserService } from "./common/services/can-activate-user.service";
import {MatSidenavModule} from '@angular/material/sidenav';

const appRoutes: Routes= [
  {
    path: '', 
    loadChildren: './home/home.module#HomeModule',
    canActivate: [CanActivateUserService]
  },
  { 
    path: 'login', 
    loadChildren: './login/login.module#LoginModule'
  },
  {
    path: 'user',
    loadChildren: './user/user.module#UserModule',
    canActivate: [CanActivateUserService]
  }
  // {
  //   path: '**',
  //   component: HotFoundComponent
  // }
];

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(appRoutes),
    HttpModule,
    MatSidenavModule
  ],
  providers: [
    { provide: ErrorHandler, useClass: AppErrorHandler },
    LoginService,
    CanActivateUserService
  ],
  bootstrap: [AppComponent]
})
export class AppModule{
  
}
