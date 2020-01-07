import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateWorkEntryComponent } from './create-work-entry.component';

describe('CreateWorkEntryComponent', () => {
  let component: CreateWorkEntryComponent;
  let fixture: ComponentFixture<CreateWorkEntryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateWorkEntryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateWorkEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
