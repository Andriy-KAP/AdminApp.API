import { Component, Input, OnInit, ViewChild, ElementRef, AfterViewInit  } from '@angular/core';
import { trigger, state, style, animate, transition } from '@angular/animations';
import { NavigationState } from "../app.service";

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styles: [`
      chart {
        display: block;
      }
     `]
})

export class HomeComponent implements OnInit {

  groupsOptions: Object;
  agentsOptions: Object;
  callsOptions: Object;
  chart: any;

  @ViewChild("groupsChart") groupsChart: any;
  @ViewChild("agentsChart") agentsChart: any;
  @ViewChild("callsChart") callsChart: any;

  constructor(){
    this.groupsOptions = {
      chart:{
        type: 'column'
      },
      title:{
        text: 'Groups performance'
      },
      rangeSelector: {
        verticalAlign: 'top',
        x: 0,
        y: 0
      },
      series:[
          {name: 'Group1', data: [{name: '10 deals has closed', y: 10, color: 'rgb(124, 181, 236)'}]},
          {name: 'Group 2', data: [{name: '4 deals has closed', y: 4, color: 'rgb(67, 67, 72)'}]},
          {name: 'Group 3', data: [{name: '7 deals has closed', y: 7, color: 'rgb(247, 163, 92)'}]},
        ]
    };
    this.agentsOptions={
      name:'agents',
      chart:{
        type: 'areaspline',
        name: 'agentsChart'
      },
      title: {
        text: 'Agents performance'
      },
      series: [
        {name: 'Agent1 name', data: [3,4,6,7,2,5]},
        {name: 'Agent2 name', data: [1,2,1,3,4,3]},
        {name: 'Agent3 name', data: [0,0,2,1,2,3]}
      ]
    };
    this.callsOptions={
          chart: { type: 'spline', name: 'callsChart' },
          title: { text : 'dynamic data example'},
          series: [{ data: [2,3,5,8,13] }]
        };
        
  }
  saveCallsInstance(chartInstance) {
        this.callsChart = chartInstance;
  }
  saveAgentsInstance(chartInstance){
        this.agentsChart=chartInstance;
  }
  saveGroupsInstance(chartInstance){
        this.groupsChart=chartInstance;
  }
  callOptionsLoad(e:any){
    debugger;
  }
  ngOnInit(){
    
  }
  ngAfterViewInit() {
    setInterval(() => {
      this.callsChart.series[0].addPoint(Math.random() * 10, true, true);
      for(let agent=0;agent<this.agentsChart.series.length;agent++){
        this.agentsChart.series[agent].addPoint(Math.random() * 10, true, true);
      }
      for(let group=0;group<this.groupsChart.series.length;group++){
        this.groupsChart.series[group].data[0].update(Math.random() * 10)//=[{name: '10 deals has closed', y: Math.random() * 10, color: 'rgb(124, 181, 236)'}];//.addPoint(Math.random() * 10, true, true);
      }
    }, 6000);
  }
}