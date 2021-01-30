import { Component, OnDestroy } from '@angular/core';
import { interval, Subject } from 'rxjs';
import { take, takeUntil } from 'rxjs/operators';
import { BatchJob } from '../shared/models/Batches';

@Component({
  selector: 'app-process',
  templateUrl: './process.component.html',
  styleUrls: ['process.component.css']
})
export class ProcessComponent implements OnDestroy {

  private readonly secondsInMilliseconds = 1000;

  private subscription = new Subject();

  public processing = false;
  public processes: BatchJob[] = [];
  public numberOfBatches = 0;
  public numberToProcess = 0;

  process() {
    this.processes = [];
    this.processing = true;

    interval(this.secondsInMilliseconds * this.numberOfBatches)
      .pipe(
        takeUntil(this.subscription),
        take(this.numberToProcess))
      .subscribe(d => {
        console.log(d);
        this.processes = [...this.processes, {
          currentBatch: d + 1,
          processedNumbers: this.numberToProcess,
          result: Math.ceil(Math.random() * 100 * this.numberToProcess)
        } as BatchJob];
        this.processing = d !== (this.numberToProcess - 1);
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

