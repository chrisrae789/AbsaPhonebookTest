import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewPhonebookEntryComponent } from './new-phonebook-entry.component';

describe('NewPhonebookEntryComponent', () => {
  let component: NewPhonebookEntryComponent;
  let fixture: ComponentFixture<NewPhonebookEntryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewPhonebookEntryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewPhonebookEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
