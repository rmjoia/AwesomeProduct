import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BatchProcessHistoryComponent } from './batch-process-history.component';

describe('BatchProcessHistoryComponent', () => {
  let component: BatchProcessHistoryComponent;
  let fixture: ComponentFixture<BatchProcessHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BatchProcessHistoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BatchProcessHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
