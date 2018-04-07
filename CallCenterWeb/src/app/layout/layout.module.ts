import { NgModule } from "@angular/core";
import { LayoutComponent } from "./layout.component";
import { Routes, RouterModule } from "@angular/router";
import { CommonModule } from '@angular/common';
import { ShortPageInfoComponent } from "../common/components/short-page-info/short-page-info.component";
import { LayoutService } from "./services/layout.service";
import { HttpClientModule } from '@angular/common/http';
import { MatProgressSpinnerModule } from "@angular/material";
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';

const routes:Routes = [
    {
        path: '',
        component: LayoutComponent
    }
];

@NgModule({
    imports:[
        CommonModule,
        RouterModule,
        HttpClientModule,
        MatProgressSpinnerModule,
        MatToolbarModule,
        MatSidenavModule
    ],
    declarations: [
        LayoutComponent,
        ShortPageInfoComponent
    ],
    exports: [
        LayoutComponent
    ],
    providers:[
        LayoutService
    ],
    bootstrap: [
        LayoutComponent
    ]
})
export class LayoutModule{

}