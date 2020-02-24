import { TestBed } from '@angular/core/testing';

import { PhonebookEntryService } from './phonebookentry.service';

describe('PhonebookentryService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PhonebookEntryService = TestBed.get(PhonebookEntryService);
    expect(service).toBeTruthy();
  });
});
