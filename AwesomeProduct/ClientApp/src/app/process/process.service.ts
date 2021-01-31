import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { BatchProcessingRequest } from '../shared/models/BatchProcessingRequest';
import { BatchProcessingResponse } from '../shared/models/BatchProcessingResponse';

@Injectable({
  providedIn: 'root'
})
export class ProcessService {
  constructor(private http: HttpClient) { }

  private readonly baseUrl = environment.processUrl;

  processBatch(processBatchRequest: BatchProcessingRequest) {
    const batchParams = `batches=${processBatchRequest.numberOfBatches}`;
    const numOfNumbersParams = `numbersToProcess=${processBatchRequest.numberToProcess}`;
    const endpointUrl = `${this.baseUrl}Dispatch?${batchParams}&${numOfNumbersParams}`;
    return this.http.get<BatchProcessingResponse>(endpointUrl);
  }

  getStatus() {
    return this.http.get<BatchProcessingResponse>(`${this.baseUrl}Dispatch/Status`);
  }
}
