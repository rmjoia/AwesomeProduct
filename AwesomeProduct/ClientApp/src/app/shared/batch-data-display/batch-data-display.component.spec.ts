import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BatchDataDisplayComponent } from './batch-data-display.component';

describe('BatchDataDisplayComponent', () => {
  let component: BatchDataDisplayComponent;
  let fixture: ComponentFixture<BatchDataDisplayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BatchDataDisplayComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BatchDataDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
