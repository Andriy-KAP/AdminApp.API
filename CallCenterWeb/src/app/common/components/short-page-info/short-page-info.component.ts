import { Component, Input } from "@angular/core";

@Component({
    selector: 'short-page-info',
    templateUrl: './short-page-component.html'
})
export class ShortPageInfoComponent{
    @Input()
    shortInfoData;
}