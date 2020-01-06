import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupsUserListComponent } from './groups-user-list.component';

describe('GroupsUserListComponent', () => {
  let component: GroupsUserListComponent;
  let fixture: ComponentFixture<GroupsUserListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GroupsUserListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupsUserListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
