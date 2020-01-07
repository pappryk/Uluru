import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageWorkingSchedulesComponent } from './manage-working-schedules.component';

describe('ManageWorkingSchedulesComponent', () => {
  let component: ManageWorkingSchedulesComponent;
  let fixture: ComponentFixture<ManageWorkingSchedulesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageWorkingSchedulesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageWorkingSchedulesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
