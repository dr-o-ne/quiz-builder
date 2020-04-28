/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { SingleChoiceRadioAnswerComponent } from './single-choice-radio-answer.component';

describe('SingleChoiceRadioAnswerComponent', () => {
  let component: SingleChoiceRadioAnswerComponent;
  let fixture: ComponentFixture<SingleChoiceRadioAnswerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SingleChoiceRadioAnswerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SingleChoiceRadioAnswerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
