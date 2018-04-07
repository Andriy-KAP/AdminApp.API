import { Directive, ElementRef } from '@angular/core';

@Directive({
    selector: '[fg-attr]'
})
export class FormGroupDirective{
    constructor(element: ElementRef){
        element.nativeElement.classList.add('form-group')
    }
}