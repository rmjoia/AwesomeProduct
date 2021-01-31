import { HttpClient } from '@angular/common/http';
import { fakeAsync, TestBed, tick } from '@angular/core/testing';
import { of } from 'rxjs';
import { BatchProcessingRequest } from '../shared/models/BatchProcessingRequest';
import { BatchProcessingResponse } from '../shared/models/BatchProcessingResponse';
import { ProcessService } from './process.service';

describe('ProcessServiceService', () => {
  const httpClientMock = jasmine.createSpyObj<HttpClient>('HttpClient', ['get', 'post']);
  let service: ProcessService;
  let httpClientSpy: jasmine.SpyObj<HttpClient>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        ProcessService,
        { provide: HttpClient, useValue: httpClientMock }
      ]
    });

    httpClientSpy = TestBed.inject(HttpClient) as jasmine.SpyObj<HttpClient>;
  });

  it('should be created', () => {
    //  Arrange
    service = TestBed.inject(ProcessService);
    //  Act

    //  Assert
    expect(service).toBeTruthy();
  });

  it('should get data when called', fakeAsync(() => {
    //  Arrange
    const expectedResult = { data: [], dateCompleted: undefined, isComplete: false } as BatchProcessingResponse;
    let data = {} as BatchProcessingResponse;
    httpClientSpy.get.and.returnValue(of(expectedResult));
    service = TestBed.inject(ProcessService);

    //  Act
    service.getStatus().subscribe(d => data = d);
    tick(500);

    //  Assert
    expect(data).toEqual({ data: [], dateCompleted: undefined, isComplete: false });
    expect(httpClientSpy.get.calls.mostRecent().args[0])
      .toEqual(
        'https://localhost:19263/Dispatch/Status'
      );
  }));

  it('should post data when called', fakeAsync(() => {
    //  Arrange
    const data = { numberOfBatches: 1, numberToProcess: 2 } as BatchProcessingRequest;
    httpClientSpy.get.and.returnValue(of(''));
    service = TestBed.inject(ProcessService);

    //  Act
    service.processBatch(data).subscribe();
    tick();

    //  Assert
    expect(httpClientSpy.get.calls.mostRecent().args[0])
      .toEqual(
        'https://localhost:19263/Dispatch?batches=1&numbersToProcess=2'
      );
  }));

});
