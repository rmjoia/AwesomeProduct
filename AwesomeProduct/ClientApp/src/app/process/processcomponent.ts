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

  private readonly UpdateIntervalInMilliseconds = 2000;

  private subscription = new Subject();

  public processing = false;
  public processes: BatchJob[] = [];
  public numberOfBatches = 0;
  public numberToProcess = 0;
  public batchTotal = 0;

  process() {
    this.processing = true;

    interval(this.UpdateIntervalInMilliseconds)
      .pipe(
        takeUntil(this.subscription),
        take(5))
      .subscribe(d => {
        console.log(d);
        this.processes = [{
          numberOfBatches: Math.ceil(Math.random() * this.numberOfBatches),
          numberToProcess: Math.ceil(Math.random() * 100 * this.numberToProcess)
        } as BatchJob];
        this.processes.map(b => this.batchTotal += b.numberToProcess);
        this.processing = d !== 4;
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

