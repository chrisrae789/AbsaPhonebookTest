import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PhonebookdetailsComponent } from './phonebookdetails.component';

describe('PhonebookdetailsComponent', () => {
  let component: PhonebookdetailsComponent;
  let fixture: ComponentFixture<PhonebookdetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PhonebookdetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PhonebookdetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
