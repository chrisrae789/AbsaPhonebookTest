import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialog, MatSnackBar } from '@angular/material';
import { PhonebookEntry } from '../phonebookentry';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';


@Component({
  selector: 'app-new-phonebook-entry',
  templateUrl: './new-phonebook-entry.component.html',
  styleUrls: ['./new-phonebook-entry.component.scss']
})


export class NewPhonebookEntryComponent implements OnInit {

  entryForm: FormGroup

  constructor(private fb: FormBuilder, private snackbar: MatSnackBar,
    public dialogRef: MatDialogRef<NewPhonebookEntryComponent>,
    @Inject(MAT_DIALOG_DATA) public data: PhonebookEntry) { }

  private onSubmit(): void {
    this.data.name = this.entryForm.get('name').value;
    var entryNumber = this.entryForm.get('phonenumber').value;
    var regex = /^[0-9\s]*$/;
    if (!regex.test(entryNumber)) {
      this.snackbar.open("Phone number must contain only digits");
      return;
    }

    this.data.phoneNumber = entryNumber;
    this.dialogRef.close(this.data);
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
    this.entryForm = this.fb.group({ name: [this.data.name, Validators.required], phonenumber: [this.data.phoneNumber, Validators.required] });
  }

}
