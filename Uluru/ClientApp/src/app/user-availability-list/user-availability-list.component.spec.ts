import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserAvailabilityListComponent } from './user-availability-list.component';

describe('UserAvailabilityListComponent', () => {
  let component: UserAvailabilityListComponent;
  let fixture: ComponentFixture<UserAvailabilityListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserAvailabilityListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserAvailabilityListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
