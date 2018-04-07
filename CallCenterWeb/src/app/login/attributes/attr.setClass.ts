import { Directive, ElementRef, Attribute, Input } from '@angular/core';

@Directive({
    selector: '[sc-attr]'
})
export class SetClassDirective {
    @Input('sc-attr')
    className: string;

    // constructor(element: ElementRef, @Attribute('sc-attr') elementClass: string){
    //     element.nativeElement.classList.add(elementClass || 'form-group ');
    // }


    constructor(private element: ElementRef){

    }
    ngOnInit(){
        this.element.nativeElement.classList.add(this.className || "form-group");
    }
}