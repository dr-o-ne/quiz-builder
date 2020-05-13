import {OnInit} from '@angular/core';
import {Choice} from 'src/app/_models/choice';
import {
  ChoicesDisplayType,
  ChoicesEnumerationType,
  BaseChoiceSettings,
  DefaultEnumChoice
} from 'src/app/_models/settings/answer.settings';
import {QuestionType, Question} from 'src/app/_models/question';

export class BaseQuestionPreviewComponent implements OnInit {
  question: Question;
  choices: Choice[];
  settings: BaseChoiceSettings;

  questionType = QuestionType;
  choicesDisplayType = ChoicesDisplayType;
  choicesEnumerationType = ChoicesEnumerationType;
  defaultEnumChoice: DefaultEnumChoice = new DefaultEnumChoice();

  enumChoice: string[];

  baseSeparator = '. ';

  ngOnInit() {
    this.initDefaults();
  }

  initDefaults(): void {
      this.initChoice();
      this.initSettings();
      this.initEnumChoice();
      this.initRandom();
  }

  initChoice(): void {
    const choices = JSON.parse(this.question.choices);
    this.choices = choices.map(choice => new Choice(choice.id, choice.text, false));
  }

  initSettings(): void {
    this.settings = JSON.parse(this.question.settings);
  }

  initEnumChoice() {
      const index = this.choicesEnumerationType[this.settings.choicesEnumerationType];
      this.enumChoice = this.defaultEnumChoice[index];
  }

  selectCorrectChoice(value: number): void {
    this.choices[value].isCorrect = true;
  }

  changeCorrectChoice(event): void {
    if (!event.value) {
        return;
    }
    this.choices.forEach(item => {
        if (item.id === event.value) {
            item.isCorrect = true;
            return;
        }
        item.isCorrect = false;
    });
  }

  initRandom() {
      if (this.question.type === this.questionType.TrueFalse) {
        return;
      }
      if (this.settings.randomize) {
        this.shuffleArray();
      }
  }

  shuffleArray() {
      this.choices.sort(() => Math.random() - 0.5);
  }
}
