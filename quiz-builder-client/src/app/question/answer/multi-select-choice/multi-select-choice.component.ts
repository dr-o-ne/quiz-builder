import { Component, OnInit, Input } from '@angular/core';
import { Answer } from 'src/app/_models/answer';
import { BaseChoiceComponent } from '../base-choice/base-choice.component';

@Component({
  selector: 'app-multi-select-choice',
  templateUrl: './multi-select-choice.component.html',
  styleUrls: ['./multi-select-choice.component.css']
})
export class MultiSelectChoiceComponent extends BaseChoiceComponent {

  constructor() {
    super();
  }

}
