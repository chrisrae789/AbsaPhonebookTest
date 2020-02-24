import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PhonebookService } from '../phonebook.service';
import { PhonebookEntryService } from '../phonebookentry.service';
import { PhonebookEntry } from '../phonebookentry';
import { Phonebook } from '../phonebook';
import { FormGroup, FormControl } from '@angular/forms';
import { Validators } from '@angular/forms';
import { MatSort, MatPaginator, MatTableDataSource, MatDialog, MatSnackBar } from '@angular/material';
import { PhonebookentrydetailsComponent } from '../phonebookentrydetails/phonebookentrydetails.component';
import { NewPhonebookEntryComponent } from '../new-phonebook-entry/new-phonebook-entry.component';
import { Title } from '@angular/platform-browser';
import { HeadingService } from '../heading.service';

@Component({
  selector: 'app-phonebookdetails',
  templateUrl: './phonebookdetails.component.html',
  styleUrls: ['./phonebookdetails.component.scss'],
  providers: [PhonebookEntryService]
})

export class PhonebookdetailsComponent implements OnInit {

  displayedColumns: string[] = ['Name', 'Number', 'Actions']
  phonebookid: string
  constructor(private route: ActivatedRoute, private router: Router, private snackbar: MatSnackBar, private dialog: MatDialog,  private heading : HeadingService, private service: PhonebookEntryService, private phonebookService: PhonebookService) {
    this.heading.setTitle("Black Books - Phonebook details");
   }
  phonebookentries: PhonebookEntry[];
  entryname: string;
  phonebook: Phonebook;
  dataSource;
  nameFilter = new FormControl('');
  numberFilter = new FormControl('');
  filterValues = {
    name: '',
    number: '',
  };
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;

  entryForm = new FormGroup(
    {
      changephonebookname: new FormControl('', Validators.required),
      entryname : new FormControl('')
    });

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  private changePhonebookName() {
    this.phonebook.name = this.entryForm.get('changephonebookname').value;
    this.phonebookService.updatePhonebook(this.phonebook).subscribe(result => {
      this.router.navigateByUrl("/openPhonebook/" + this.phonebook.id + "/" + this.phonebook.name);
    });
    this.snackbar.open("Phonebook name changed to " + this.phonebook.name);
  }

  private deletePhonebookEntry(value: PhonebookEntry) {
    this.service.deletePhonebookEntryById(value.id).subscribe(result => {
      this.snackbar.open("Deleted phonebook entry - " + value.name);
      this.refreshEntryList();
    }, error => console.error(error));
  }

  private createPhonebookEntry() {
    this.dialog.open(NewPhonebookEntryComponent, { data: new PhonebookEntry() }).afterClosed().subscribe(result => {
      if (result != null) {
        let newEntry = result;
        newEntry.phonebookId = this.phonebookid;
        this.service.createPhonebookEntry(newEntry).subscribe(result => {
          this.snackbar.open("Creating entry...");
          this.refreshEntryList();
        });
      }
    });
  }

  private refreshEntryList() {
    this.service.setPhonebook(this.phonebookid)
    this.service.getPhonebookEntries(this.entryname).subscribe(result => {
      this.phonebookentries = result;
      this.dataSource = new MatTableDataSource<PhonebookEntry>(result as PhonebookEntry[]);
      this.dataSource.filterPredicate = this.createFilter();
      this.dataSource.paginator = this.paginator;
    }, error => console.error(error));
  }

  private loadPhonebook() {
    this.phonebookService.getPhonebookById(this.phonebookid).subscribe(result => {
      this.phonebook = result;
      this.heading.setTitle("Black Books - Phonebook details for "+ this.phonebook.name);
    }, error => console.error(error));
  }

  private search() {
    this.entryname = this.entryForm.get("entryname").value;
    this.refreshEntryList();
  }

  createFilter(): (data: PhonebookEntry, filter: string) => boolean {
    let filterFunction = function (data, filter): boolean {
      let searchTerms = JSON.parse(filter);
      return data.name.toLowerCase().indexOf(searchTerms.name) !== -1
        && data.phoneNumber.toString().toLowerCase().indexOf(searchTerms.number) !== -1;
    }
    return filterFunction;
  }
  ngOnInit() {
    this.phonebook = null;
    this.entryname = "";
    this.route.params.subscribe(params => { this.phonebookid = params['phonebookid'] })

    this.nameFilter.valueChanges
      .subscribe(
        name => {
          this.filterValues.name = name;
          this.dataSource.filter = JSON.stringify(this.filterValues);
        }
      )

    this.numberFilter.valueChanges
      .subscribe(
        number => {
          this.filterValues.number = number;
          this.dataSource.filter = JSON.stringify(this.filterValues);
        }
      )
    this.loadPhonebook();
    this.refreshEntryList();
  }
}

