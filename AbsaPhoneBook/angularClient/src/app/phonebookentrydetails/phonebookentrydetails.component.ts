import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { map } from 'rxjs/operators';
import { PhonebookEntryService } from '../phonebookentry.service';
import { PhonebookEntry } from '../phonebookentry';
import { PhonebookService } from '../phonebook.service';
import { Location } from '@angular/common';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { HeadingService } from '../heading.service';

@Component({
  selector: 'app-phonebookentrydetails',
  templateUrl: './phonebookentrydetails.component.html',
  styleUrls: ['./phonebookentrydetails.component.scss'],
  providers: [PhonebookEntryService]
})

export class PhonebookentrydetailsComponent implements OnInit {
  fromurl: string;
  entryId: string;
  entry = new PhonebookEntry();
  phonebookid: string;
  constructor(private fb: FormBuilder, private location: Location, private router: Router, private route: ActivatedRoute, public snackbar: MatSnackBar, private heading : HeadingService, private entryservice: PhonebookEntryService, private phonebookservice: PhonebookService) { }

  entryForm = new FormGroup(
    {
      name: new FormControl('', Validators.required),
      phonenumber: new FormControl('', Validators.required),

    });

  phonebookname: string;
  entryname: string;

  private backToPhonebook() {
    this.location.back();
  }

  submit() {
    this.entry.name = this.entryForm.get('name').value;
    var entryNumber = this.entryForm.get('phonenumber').value;

    var regex = /^[0-9\s]*$/;
    if (!regex.test(entryNumber))
    this.snackbar.open("Phone number must contain only digits");
    else {
      this.entry.phoneNumber = entryNumber;

      this.entryservice.updatePhonebookEntry(this.entry).subscribe(result => {
        completed:
        {
          this.snackbar.open("Updating entry...{{result.name}}");
        }
        error: error => console.error(error)
      });

    }
  }

  ngOnInit() {
    this.route.params.pipe(map(params => { this.entryId = params['entryid'], this.phonebookid = params['phonebookid'] }))
      .subscribe(result => {
        error: error => console.error(error)
        completed: this.entryservice.getPhonebookEntryById(this.entryId).subscribe(result => {
          this.entry = result;
          this.phonebookservice.getPhonebookById(this.entry.phonebookId).subscribe(result => {
            this.phonebookname = result.name;
            this.heading.setTitle("Editing details of "+ this.entry.name);
            this.entryForm = this.fb.group({ name: [this.entry.name, Validators.required], phonenumber: [this.entry.phoneNumber, Validators.required] });
          });
        });
      },

      );

  }
}

