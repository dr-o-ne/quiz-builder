import {Component} from '@angular/core';
import { ChoiceBaseDirective } from '../choice-base.directive';
import { OptionItem } from 'src/app/_models/UI/optionItem';

@Component({
  selector: 'app-true-false-choice',
  templateUrl: './true-false-choice.component.html',
  styleUrls: ['./true-false-choice.component.css']
})

export class TrueFalseChoiceComponent extends ChoiceBaseDirective{

  options: OptionItem[] = [];

}
