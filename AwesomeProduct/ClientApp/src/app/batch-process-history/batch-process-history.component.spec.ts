import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { BatchProcessingResponse } from '../shared/models/BatchProcessingResponse';
import { HistoryService } from '../shared/Services/history-service.service';

import { BatchProcessHistoryComponent } from './batch-process-history.component';

describe('BatchProcessHistoryComponent', () => {
  const historyServiceMock = jasmine.createSpyObj<HistoryService>('HistoryService', ['getLastProcess']);
  let component: BatchProcessHistoryComponent;
  let fixture: ComponentFixture<BatchProcessHistoryComponent>;
  let historyServiceSpy: jasmine.SpyObj<HistoryService>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BatchProcessHistoryComponent],
      providers: [
        { provide: HistoryService, useValue: historyServiceMock }
      ]
    })
      .compileComponents();

    historyServiceSpy = TestBed.inject(HistoryService) as jasmine.SpyObj<HistoryService>;
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BatchProcessHistoryComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    //  Arrange
    const response = { data: [], dateCompleted: undefined, isComplete: false } as BatchProcessingResponse;
    historyServiceSpy.getLastProcess.and.returnValue(of(response));

    //  Act
    fixture.detectChanges();
    //  Assert
    expect(component).toBeTruthy();
  });
});
