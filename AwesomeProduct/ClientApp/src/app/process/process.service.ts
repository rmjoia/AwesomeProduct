import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { BatchProcessingRequest } from '../shared/models/BatchProcessingRequest';
import { BatchProcessingResponse } from '../shared/models/BatchProcessingResponse';

@Injectable({
  providedIn: 'root'
})
export class ProcessService {
  constructor(private http: HttpClient) { }

  processBatch(processBatchRequest: BatchProcessingRequest) {
    const batchParams = `batches=${processBatchRequest.numberOfBatches}`;
    const numOfNumbersParams = `numbersToProcess=${processBatchRequest.numberToProcess}`;
    const endpointUrl = `/Process?${batchParams}&${numOfNumbersParams}`;
    return this.http.get<BatchProcessingResponse>(endpointUrl);
  }

  getStatus() {
    return this.http.get<BatchProcessingResponse>('Process/Status');
  }
}
