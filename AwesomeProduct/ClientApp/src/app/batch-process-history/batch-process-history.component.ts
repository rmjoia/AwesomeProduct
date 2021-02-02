import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BatchProcessingResponse } from '../shared/models/BatchProcessingResponse';
import { HistoryService } from '../shared/Services/history-service.service';

@Component({
  selector: 'app-batch-process-history',
  templateUrl: './batch-process-history.component.html',
  styleUrls: ['./batch-process-history.component.css']
})
export class BatchProcessHistoryComponent implements OnInit {

  private readonly initialState = {
    data: [],
    isComplete: false,
    dateCompleted: undefined
  } as BatchProcessingResponse;

  processes$: Observable<BatchProcessingResponse> = of(this.initialState);
  numberOfBatches = 0;
  numberToProcess = 0;

  constructor(private historyService: HistoryService) { }

  ngOnInit(): void {
    this.processes$ = this.historyService.getLastProcess()
      .pipe(
        catchError(() => {
          return of(this.initialState);
        })
      );
  }

}
