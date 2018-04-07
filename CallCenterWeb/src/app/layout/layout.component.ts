import { Component, Input, OnInit, ViewChild, ElementRef, AfterViewInit, ContentChild } from '@angular/core';
import { trigger, state, style, animate, transition } from '@angular/animations';
import { NavigationState } from "../app.service";
import { LayoutService } from "./services/layout.service";
import { ResponseModel } from "../common/models/response.model";
import { AppError } from "../common/models/app-error";
import { UserComponent } from "../user/user.component";

@Component({
    selector: 'layout',
    templateUrl: './layout.component.html',
    styles: [`
      chart {
        display: block;
      }
    `],
    animations: [
    trigger('asideNavState',
    [
      state('initial', style({
        width: '0px'
      })),
      state('finite', style({
        width: '200px'
      })),
      transition('initial => finite', animate('100ms ease-in')),
      transition('finite => initial', animate('100ms ease-out'))
    ]),
    trigger('mainSectionState',
    [
      state('initial', style({
        marginLeft: '0px'
      })),
      state('finite', style({
        marginLeft: '200px'
      })),
      transition('initial => finite', animate('100ms ease-in')),
      transition('finite => initial', animate('100ms ease-out'))
    ])
  ]
})
export class LayoutComponent implements OnInit{
    title = 'app';
    navState= 'initial';
    mainSectionState ='initial';
    shortInfoData: {};
    isComplete: boolean = false;

    @Input() asideNav: NavigationState;

    constructor(private service: LayoutService){
            
    }    
    ngOnInit(){
        this.getLayoutInfo();
    }
   
    onDrillDown(e){
        console.log(e);
    }
    toggleState(){
      this.navState= this.navState === 'initial' ? 'finite' : 'initial';
      this.mainSectionState = this.navState === 'initial' ? 'initial' : 'finite';
    }

    getLayoutInfo(){
      this.service.getLayoutInfo()
        .subscribe((data: ResponseModel)=>{
          if(data.data != null){
            console.log(data);
            this.shortInfoData= data;
          }
        }, (error)=>{
          throw error;
        });
    }
   
}