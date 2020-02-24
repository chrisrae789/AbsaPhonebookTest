import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PhonebookentrydetailsComponent } from './phonebookentrydetails.component';

describe('PhonebookentrydetailsComponent', () => {
  let component: PhonebookentrydetailsComponent;
  let fixture: ComponentFixture<PhonebookentrydetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PhonebookentrydetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PhonebookentrydetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
