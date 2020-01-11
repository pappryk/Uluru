import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditWorkEntryComponent } from './edit-work-entry.component';

describe('EditWorkEntryComponent', () => {
  let component: EditWorkEntryComponent;
  let fixture: ComponentFixture<EditWorkEntryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditWorkEntryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditWorkEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
