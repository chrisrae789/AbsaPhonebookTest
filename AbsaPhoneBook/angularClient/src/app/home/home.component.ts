import { Component, ViewChild } from '@angular/core';
import {PhonebookService} from '../phonebook.service';
import {Phonebook} from '../phonebook';
import {Title} from "@angular/platform-browser";
import { MatSort, MatPaginator, MatTableDataSource, MatDialog, MatSnackBar, MatSnackBarConfig,  } from '@angular/material';
import { HeadingService } from '../heading.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  providers: [PhonebookService]
})

export class HomeComponent  {
  constructor( private snackbar: MatSnackBar, private titleService:Title, private service: PhonebookService,  private heading : HeadingService) {
    this.titleService.setTitle("Black Books - Phonebooks");
   this.heading.setTitle("Black Books - Phonebooks");
  }
  displayedColumns: string[] = ['Name', 'Actions']
  phonebooks: Phonebook[] ;
  phonebookname : string;
  newphonebookname : string;
  dataSource;

  @ViewChild(MatSort,  {static: false}) sort: MatSort;
  @ViewChild(MatPaginator,  {static: false}) paginator: MatPaginator;

  deletePhonebook(value : Phonebook)
  {
    this.snackbar.open("Deleted phonebook - "+ value.name);
    this.service.deletePhonebookById(value.id).subscribe(result => {
      this.refreshPhonebookList();
    }, error => console.error(error));
  }

  addPhonebook()
  {
    if(this.newphonebookname == "")
    {
      this.snackbar.open("A phonebook requires a name");
      return;
    }

     this.snackbar.open("Creating new phonebook - " + this.newphonebookname);

    newBook : Phonebook;
    this.service.createPhonebook(this.newphonebookname).subscribe(result => {
      this.refreshPhonebookList();
    }, error => console.error(error));

  }

  search()
  {
    this.refreshPhonebookList();
  }

  private refreshPhonebookList()
  {
       this.service.getPhonebooks(this.phonebookname).subscribe(result => {
      this.phonebooks = result;
      this.dataSource = new MatTableDataSource<Phonebook>(result as Phonebook[]);
      this.dataSource.paginator = this.paginator;
    }, error => console.error(error));
  }

  ngOnInit()
  {
    this.newphonebookname="";
    this.phonebookname ="";
    this.refreshPhonebookList();
  }

  title = 'Black Books';
}




