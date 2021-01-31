import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { BatchJobResult } from '../models/BatchJobResult';
import { BatchProcessingRequest } from '../models/BatchProcessingRequest';
import { BatchProcessingResponse } from '../models/BatchProcessingResponse';

@Injectable({
  providedIn: 'root'
})
export class HistoryService {
  constructor(private http: HttpClient) { }

  private readonly baseUrl = environment.persistenceUrl;

  saveBatchProcess(batchProcessingResponse: BatchProcessingResponse) {
    const payload = {
      data: batchProcessingResponse.data,
      dateCompleted: batchProcessingResponse.DateCompleted
    };

    return this.http.post<BatchJobResult[]>(`${this.baseUrl}BatchJobs`, payload, {});
  }

  getLastProcess(): Observable<BatchProcessingResponse> {
    return this.http.get<BatchProcessingResponse>(`${this.baseUrl}BatchJobs`);
  }
}
