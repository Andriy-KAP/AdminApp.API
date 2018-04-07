import { Component, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";

@Component({
    selector: 'edit-user',
    templateUrl: './edit-user.component.html'
})
export class EditUserComponent{
    constructor(public dialogRef:MatDialogRef<EditUserComponent>, @Inject(MAT_DIALOG_DATA) public data: any)
    {

    }
    onSaveClick(): void{
        this.dialogRef.close();
    }
}