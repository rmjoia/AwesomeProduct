import { Component, OnDestroy } from '@angular/core';
import { interval, Subject } from 'rxjs';
import { take, takeUntil } from 'rxjs/operators';
import { BatchJobResult } from '../shared/models/BatchJobResult';
import { BatchProcessingResponse } from '../shared/models/BatchProcessingResponse';

@Component({
  selector: 'app-process',
  templateUrl: './process.component.html',
  styleUrls: ['process.component.css']
})
export class ProcessComponent implements OnDestroy {

  private readonly initialState = { BatchJobs: [], isComplete: false } as BatchProcessingResponse;
  private readonly secondsInMilliseconds = 1000;
  private subscription = new Subject();

  public processing = false;
  public data: BatchProcessingResponse = this.initialState;
  public numberOfBatches = 0;
  public numberToProcess = 0;

  process() {
    this.data = this.initialState;
    this.processing = true;

    interval(this.secondsInMilliseconds * this.numberOfBatches)
      .pipe(
        takeUntil(this.subscription),
        take(this.numberToProcess))
      .subscribe(d => {
        console.log(d);
        this.data = {
          ...this.data,
          BatchJobs: [
            {
              batchNumber: d + 1,
              leftToProcess: 0,
              result: Math.ceil(Math.random() * 100 * this.numberToProcess)
            } as BatchJobResult,
          ],
          isComplete: d !== (this.numberToProcess - 1)
        } as BatchProcessingResponse;
      });
  }

  isValid() {
    return this.numberOfBatches &&
      this.numberToProcess;
  }
  ngOnDestroy(): void {
    this.subscription.next();
    this.subscription.complete();
  }
}

