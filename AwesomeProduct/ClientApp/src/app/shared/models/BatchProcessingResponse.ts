import { BatchJobResult } from './BatchJobResult';

export interface BatchProcessingResponse {
  data: BatchJobResult[];
  isComplete: boolean;
  DateCompleted: Date | undefined;
}
