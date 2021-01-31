import { Component, OnDestroy } from '@angular/core';
import { EMPTY, interval, Observable, of, Subject } from 'rxjs';
import { catchError, take, takeUntil } from 'rxjs/operators';
import { BatchProcessingRequest } from '../shared/models/BatchProcessingRequest';
import { BatchProcessingResponse } from '../shared/models/BatchProcessingResponse';
import { HistoryService } from '../shared/Services/history-service.service';
import { ProcessService } from './process.service';

@Component({
  selector: 'app-process',
  templateUrl: './process.component.html',
  styleUrls: ['process.component.css']
})
export class ProcessComponent implements OnDestroy {

  private readonly initialState = {
    data: [],
    isComplete: false,
    DateCompleted: undefined
  } as BatchProcessingResponse;
  private readonly secondsInMilliseconds = 1000;
  private subscription = new Subject();

  public processing = false;
  public saving = false;
  public response: BatchProcessingResponse = this.initialState;
  public numberOfBatches = 0;
  public numberToProcess = 0;

  constructor(private processService: ProcessService, private historyService: HistoryService) {

  }

  process() {
    this.response = this.initialState;
    this.processing = true;
    this.subscription = new Subject();

    this.processService.processBatch(
      {
        numberOfBatches: this.numberOfBatches,
        numberToProcess: this.numberToProcess
      } as BatchProcessingRequest
    ).pipe(
      take(1),
      catchError(this.handleError.bind(this))
    ).subscribe();

    this.pollForData();
  }

  pollForData() {
    interval(this.secondsInMilliseconds * 2)
      .pipe(
        takeUntil(this.subscription),
        catchError(this.handleError.bind(this)))
      .subscribe(_ => {
        this.getStatus();
      });
  }

  getStatus() {
    this.processService.getStatus().subscribe(response => {

      this.response = { ...response };

      if (this.response.isComplete) {
        this.unSubscribe();
        this.processing = false;
        this.persistData(response);
      }

    });
  }

  persistData(response: BatchProcessingResponse) {
    this.saving = true;
    this.historyService
      .saveBatchProcess(response)
      .pipe(
        catchError(error => {
          console.error(error);
          this.saving = false;
          return EMPTY;
        })
      ).subscribe(_ => {
        this.saving = false;
      });
  }


  isValid() {
    return this.numberOfBatches &&
      this.numberToProcess;
  }

  handleError(error: Observable<any>): Observable<any> {
    console.error(error);
    this.subscription.next();
    this.subscription.complete();
    this.processing = false;
    return of(this.initialState);
  }

  unSubscribe() {
    this.subscription.next();
    this.subscription.complete();
  }

  ngOnDestroy(): void {
    this.unSubscribe();
  }
}

