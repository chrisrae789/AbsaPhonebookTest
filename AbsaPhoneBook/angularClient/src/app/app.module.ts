import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { PhonebookdetailsComponent } from './phonebookdetails/phonebookdetails.component';
import { HomeComponent } from './home/home.component';
import { PhonebookentrydetailsComponent } from './phonebookentrydetails/phonebookentrydetails.component';
import { ReactiveFormsModule, FormsModule  } from '@angular/forms';
import {MatButtonModule, MatInputModule, MatCardModule, MatSelectModule,
  MatTableModule, MatToolbarModule, MatDialogModule, MatListModule,
MatSortModule, MatPaginatorModule, MatIconModule, MatSnackBarModule, MAT_SNACK_BAR_DEFAULT_OPTIONS} from '@angular/material';
import { HeaderComponent } from './header/header.component';
import { NewPhonebookEntryComponent } from './new-phonebook-entry/new-phonebook-entry.component';

@NgModule({
  declarations: [
    AppComponent,
    PhonebookdetailsComponent,
    HomeComponent,
    PhonebookentrydetailsComponent,
    HeaderComponent,
    NewPhonebookEntryComponent,

  ],
  imports: [
    BrowserModule,
    HttpClientModule,
        //material design
        BrowserAnimationsModule, MatButtonModule, MatTableModule,
        MatInputModule, MatCardModule, MatSelectModule, MatToolbarModule,
        MatDialogModule, MatListModule, MatSortModule, MatPaginatorModule,
        MatIconModule,MatSnackBarModule,

    NgbModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [{provide: MAT_SNACK_BAR_DEFAULT_OPTIONS, useValue: {duration: 2500}}],
  entryComponents: [NewPhonebookEntryComponent],
  bootstrap: [AppComponent],
})
export class AppModule { }
