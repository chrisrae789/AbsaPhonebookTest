import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Phonebook } from './phonebook';

@Injectable({
  providedIn: 'root'
})

export class PhonebookService {
  url = '/api/phonebook/';

  constructor(private http: HttpClient) { }
  getPhonebooks(value : string): Observable<Phonebook[]> {
       return this.http.get<Phonebook[]>(this.url + "?name="+  value);
  }
  getPhonebookById(phonebookId: string): Observable<Phonebook> {
    return this.http.get<Phonebook>(this.url + phonebookId);
  }
  createPhonebook(value : string): Observable<Phonebook> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<Phonebook>(this.url + "create/" + value, httpOptions);
  }
  updatePhonebook(phonebook: Phonebook): Observable<Phonebook> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put<Phonebook>(this.url + "update/"+ phonebook.id,
      phonebook, httpOptions);
  }
  deletePhonebookById(phonebookid: string) {
    return this.http.delete(this.url + phonebookid);
  }
}
