import { Component, Input } from '@angular/core';
import { groupBy } from 'rxjs/operators';
import { BatchJobResult } from '../models/BatchJobResult';

@Component({
  selector: 'app-batch-data-display',
  templateUrl: './batch-data-display.component.html',
  styleUrls: ['./batch-data-display.component.css']
})
export class BatchDataDisplayComponent {

  @Input() batches: BatchJobResult[] = [];
  @Input() numberOfBatches = 0;
  @Input() numberToProcess = 0;

  batchSumTotal(): number {

    let total = 0;

    this.batches?.map(b => total += b.number);

    return total;
  }

}
