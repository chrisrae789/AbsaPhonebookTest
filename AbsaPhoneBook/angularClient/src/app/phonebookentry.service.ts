import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PhonebookEntry } from './phonebookentry';

@Injectable({
  providedIn: 'root'
})

export class PhonebookEntryService {
  url = 'api/phonebook/entries/';


  private currentPhonebookId: string
  public setPhonebook(value: string) {
    this.currentPhonebookId = value;
  }

  constructor(private http: HttpClient) { }
  getPhonebookEntries(value : string): Observable<PhonebookEntry[]> {
    return this.http.get<PhonebookEntry[]>(this.url + 'byphonebook/' + this.currentPhonebookId + "?name=" + value);
  }
  getPhonebookEntryById(entryId: string): Observable<PhonebookEntry> {
    return this.http.get<PhonebookEntry>(this.url + entryId);
  }
  createPhonebookEntry(phonebookEntry: PhonebookEntry): Observable<PhonebookEntry> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<PhonebookEntry>(this.url + "byphonebook/create/" +  phonebookEntry.phonebookId,
      phonebookEntry, httpOptions);
  }
  updatePhonebookEntry(phonebookEntry: PhonebookEntry): Observable<PhonebookEntry> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put<PhonebookEntry>(this.url + 'byphonebook/update/' +  phonebookEntry.phonebookId,
      phonebookEntry, httpOptions);
  }
  deletePhonebookEntryById(phonebookentryid: string): Observable<number> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.delete<number>(this.url + phonebookentryid,
      httpOptions);
  }
}
