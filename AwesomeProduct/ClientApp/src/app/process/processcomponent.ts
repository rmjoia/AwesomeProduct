import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { interval, Observable, of, Subject } from 'rxjs';

@Component({
  selector: 'app-process',
  templateUrl: './process.component.html',
  styleUrls: ['process.component.css']
})
export class ProcessComponent implements OnDestroy {

  private subscription = new Subject();

  public processing: WeatherForecast[] = [];
  public numberOfBatches: number;
  public numberToProcess: number;

  process() {
    console.log(this.numberOfBatches, this.numberToProcess);

    interval(100)
    .pipe(takeUntil())
    .subscribe();
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

