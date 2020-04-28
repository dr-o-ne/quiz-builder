/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { SingleChoiceDropdownAnswerComponent } from './single-choice-dropdown-answer.component';

describe('SingleChoiceDropdownAnswerComponent', () => {
  let component: SingleChoiceDropdownAnswerComponent;
  let fixture: ComponentFixture<SingleChoiceDropdownAnswerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SingleChoiceDropdownAnswerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SingleChoiceDropdownAnswerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
