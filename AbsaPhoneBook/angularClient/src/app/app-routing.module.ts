import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {HomeComponent} from './home/home.component';

import { PhonebookdetailsComponent } from './phonebookdetails/phonebookdetails.component';
import { PhonebookentrydetailsComponent } from './phonebookentrydetails/phonebookentrydetails.component';

const routes: Routes = [
  { path: '', component: HomeComponent},
  { path: 'home', component: HomeComponent},
  { path: 'openPhonebook/:phonebookid/:phonebookname', component: PhonebookdetailsComponent},
  { path: 'openPhonebookEntry/:entryid', component: PhonebookentrydetailsComponent },
  { path: 'newPhonebookEntry/:phonebookid', component: PhonebookentrydetailsComponent  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
