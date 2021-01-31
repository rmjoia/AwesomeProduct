import { TestBed } from '@angular/core/testing';

import { HistoryService } from './history-service.service';

describe('HistoryServiceService', () => {
  let service: HistoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HistoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
