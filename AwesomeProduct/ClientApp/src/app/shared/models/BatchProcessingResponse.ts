import { BatchJobResult } from './BatchJobResult';

export interface BatchProcessingResponse {
  BatchJobs: BatchJobResult[];
  isComplete: boolean;
}
