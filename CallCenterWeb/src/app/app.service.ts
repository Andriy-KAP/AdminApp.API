export class NavigationState{
    constructor(public name: string, public state = 'initial'){
        console.log('service');
    }

    toggleState(){
        this.state= this.state === 'initial' ? 'finite' : 'initial';
    }
}