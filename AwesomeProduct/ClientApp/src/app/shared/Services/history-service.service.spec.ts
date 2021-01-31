import { HttpClient } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { BatchProcessingResponse } from '../models/BatchProcessingResponse';

import { HistoryService } from './history-service.service';

describe('HistoryServiceService', () => {
  const httpClientSpy = jasmine.createSpyObj<HttpClient>('HttpClient', ['get', 'post']);
  let service: HistoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        { provide: HttpClient, useValue: httpClientSpy }
      ]
    });
  });

  it('should be created', () => {
    //  Arrange
    service = TestBed.inject(HistoryService);
    //  Act

    //  Assert
    expect(service).toBeTruthy();
  });

  it('should get data when called', () => {
    //  Arrange
    const expectedResult = { data: [], dateCompleted: undefined, isComplete: false } as BatchProcessingResponse;
    let data = {} as BatchProcessingResponse;
    httpClientSpy.get.and.returnValue(of(expectedResult));
    service = TestBed.inject(HistoryService);

    //  Act
    service.getLastProcess().subscribe(d => data = d);

    //  Assert
    expect(data).toEqual({ data: [], dateCompleted: undefined, isComplete: false });
    expect(httpClientSpy.get).toHaveBeenCalledTimes(1);
  });

  it('should post data when called', () => {
    //  Arrange
    const data = { data: [], dateCompleted: undefined, isComplete: true } as BatchProcessingResponse;
    httpClientSpy.post.and.returnValue(of(''));
    service = TestBed.inject(HistoryService);

    //  Act
    service.saveBatchProcess(data).subscribe();

    //  Assert
    expect(httpClientSpy.post)
      .toHaveBeenCalledOnceWith(
        'https://localhost:5001/BatchJobs',
        { data: [], dateCompleted: undefined },
        {}
      );
  });

});
