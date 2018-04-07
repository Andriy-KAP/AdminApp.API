import { Component, OnInit, Injectable, ViewChild, EventEmitter, Output } from '@angular/core';
import { UserService } from "./services/user.service";
import { ResponseModel } from "../common/models/response.model";
import { MatPaginator, MatTableDataSource, MatSort, MatDialog } from "@angular/material";
import {Observable} from 'rxjs/Observable';
import {merge} from 'rxjs/observable/merge';
import {of as observableOf} from 'rxjs/observable/of';
import {catchError} from 'rxjs/operators/catchError';
import {map} from 'rxjs/operators/map';
import {startWith} from 'rxjs/operators/startWith';
import {switchMap} from 'rxjs/operators/switchMap';
import { PaginationService } from "../common/services/pagination.service";
import { Pagination } from "../common/models/pagination.model";
import { EditUserComponent } from "./components/edit/edit-user.component";

export interface Element{
  email: string;
  hashedPAssword: number;
  password: string;
}

@Component({
    selector: 'user',
    templateUrl: './user.component.html'
})

@Injectable()
export class UserComponent implements OnInit{
    @ViewChild(MatPaginator) paginator : MatPaginator;
    @ViewChild(MatSort) sort: MatSort
    pagination: Pagination<Element>;
    users: any;
    isActive: boolean = true;
    isComplete: boolean = false;

    constructor(private service:UserService, public dialog: MatDialog){
      this.pagination = new Pagination<Element>();
      this.pagination.isLoadingResults = true;
      this.pagination.columns = ['email','hashedPassword','password', 'actions'];
    }

    ngOnInit(){
      
    }
    ngAfterViewInit(){
        this.service.getUsers(this.paginator.pageIndex + 1, this.paginator.pageSize)
        .subscribe((response : any)=>{
          this.users = response.data.items;   
          this.pagination.dataCount = response.data.totalCount;
          this.pagination.dataSource = new MatTableDataSource<Element>(this.users);
          this.pagination.isLoadingResults = false;
          this.pagination.paginator = this.paginator;
          this.pagination.sort= this.sort;
          this.isComplete = true;
        });
    }
    onPageChanged(context){
      this.pagination.isLoadingResults = true;
      this.service.getUsers(context.pageIndex + 1, context.pageSize)
        .subscribe((response : any)=>{
          this.users = response.data.items;
          this.pagination.dataSource = new MatTableDataSource<Element>(this.users);
          this.pagination.dataSource.sort = this.sort,
          this.pagination.isLoadingResults = false;
        });   
    }
    applyFilter(filterValue: string) {
      filterValue = filterValue.trim(); // Remove whitespace
      filterValue = filterValue.toLowerCase(); // MatTableDataSource defaults to lowercase matches
      this.pagination.dataSource.filter = filterValue;
    }
    edit(){
      let dialogRef = this.dialog.open(EditUserComponent, {
      width: '550px',
      data: { name: 'sdsd', animal: 'dsdsd' }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      //this.animal = result;
      });
    } 
}