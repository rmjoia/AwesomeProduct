import { Component, OnInit } from '@angular/core';
import { BatchJobResult } from '../shared/models/BatchJobResult';
import { BatchProcessingResponse } from '../shared/models/BatchProcessingResponse';

@Component({
  selector: 'app-batch-process-history',
  templateUrl: './batch-process-history.component.html',
  styleUrls: ['./batch-process-history.component.css']
})
export class BatchProcessHistoryComponent implements OnInit {

  processes: BatchProcessingResponse[] = [];
  numberOfBatches = 0;
  numberToProcess = 0;

  constructor() { }

  ngOnInit(): void {
    //  fetch the latest result
    this.numberOfBatches = 1;
    this.numberToProcess = 1;
    this.processes = [
      {
        data: [
          { batchNumber: 1, number: 500, leftToProcess: 0 } as BatchJobResult
        ],
        isComplete: true
      } as BatchProcessingResponse
    ];

  }

}
