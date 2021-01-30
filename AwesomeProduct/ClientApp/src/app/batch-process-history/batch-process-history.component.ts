import { Component, OnInit } from '@angular/core';
import { BatchJob } from '../shared/models/Batches';

@Component({
  selector: 'app-batch-process-history',
  templateUrl: './batch-process-history.component.html',
  styleUrls: ['./batch-process-history.component.css']
})
export class BatchProcessHistoryComponent implements OnInit {

  processes: BatchJob[] = [];
  numberOfBatches = 0;
  numberToProcess = 0;

  constructor() { }

  ngOnInit(): void {
    //  fetch the latest result
    this.numberOfBatches = 1;
    this.numberToProcess = 1;
    this.processes = [
      {
        currentBatch: 1,
        processedNumbers: 1,
        result: 500
      } as BatchJob
    ];

  }

}
