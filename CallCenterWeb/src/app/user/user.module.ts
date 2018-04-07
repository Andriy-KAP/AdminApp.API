import { NgModule } from '@angular/core';
import { Routes, RouterModule } from "@angular/router";
import { UserComponent } from "./user.component";
import { CommonModule } from "@angular/common";
import { UserService } from "./services/user.service";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { LayoutModule } from "../layout/layout.module";
import { MatTableModule } from '@angular/material/table';
import { MatInputModule, MatPaginatorModule, MatProgressSpinnerModule, MatSortModule, MatButtonModule,MatIconModule, MatDialogModule } from '@angular/material';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { PaginationService } from "../common/services/pagination.service";
import { DataService } from "../common/services/data.service";
import { AuthInterceptor } from "../common/interceptors/auth.interceptor";
import { LoaderComponent } from "../common/components/loader/loader.component";

const routes: Routes =[
    {
        path: '',
        component: UserComponent
    }
]

@NgModule({
    declarations: [
        UserComponent,
        LoaderComponent
    ],
    imports:[
        CommonModule,
        RouterModule.forChild(routes),
        HttpClientModule,
        LayoutModule,
        FormsModule,
        ReactiveFormsModule,
        MatTableModule,
        MatInputModule,
        MatPaginatorModule,
        MatProgressSpinnerModule,
        MatSortModule,
        MatButtonModule,
        MatIconModule,
        MatDialogModule
    ],
    providers: [
        UserService,
        PaginationService,
        DataService,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            multi: true
        }
    ],
    bootstrap:[]
})
export class UserModule{

}