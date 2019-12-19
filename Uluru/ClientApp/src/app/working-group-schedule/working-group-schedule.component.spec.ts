import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkingGroupScheduleComponent } from './working-group-schedule.component';

describe('WorkingGroupScheduleComponent', () => {
  let component: WorkingGroupScheduleComponent;
  let fixture: ComponentFixture<WorkingGroupScheduleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WorkingGroupScheduleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkingGroupScheduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
