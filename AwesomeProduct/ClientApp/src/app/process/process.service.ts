import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BatchProcessingRequest } from '../shared/models/BatchProcessingRequest';

@Injectable({
  providedIn: 'root'
})
export class ProcessService {
  constructor(private http: HttpClient) { }

  processBatch(processBatchRequest: BatchProcessingRequest) {
    const batchParams = `numberOfBatches${processBatchRequest.numberOfBatches}`;
    const numOfNumbersParams = `numberToProcess=${processBatchRequest.numberToProcess}`;
    const endpointUrl = `/process?${batchParams}&${numOfNumbersParams}`;
    return this.http.get(endpointUrl);
  }
}
