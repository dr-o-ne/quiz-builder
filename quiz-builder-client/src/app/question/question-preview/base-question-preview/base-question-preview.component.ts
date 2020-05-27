import {OnInit, Injector} from '@angular/core';
import {Choice} from 'src/app/_models/choice';
import {
  ChoicesDisplayType,
  ChoicesEnumerationType,
  BaseChoiceSettings,
  DefaultEnumChoice
} from 'src/app/_models/settings/answer.settings';
import {QuestionType, Question} from 'src/app/_models/question';
import { InfoChoice } from 'src/app/_models/option';
import { AttemptService } from 'src/app/_service/attempt.service';

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

  constructor(
    private attemptService: AttemptService
  ) {}

  ngOnInit(): void {
    this.initDefaults();
  }

  initDefaults(): void {
      this.initChoice();
      this.initSettings();
      this.initEnumChoice();
      this.initRandom();
  }

  initChoice(): void {
    if ( !this.choices ) {
      const choices = JSON.parse(this.question.choices);
      this.choices = choices.map(choice => new Choice(choice.id, choice.text, false));
    }
  }

  initSettings(): void {
    this.settings = JSON.parse(this.question.settings);
  }

  initEnumChoice(): void {
      const index = this.choicesEnumerationType[this.settings.choicesEnumerationType];
      this.enumChoice = this.defaultEnumChoice[index];
  }

  selectCorrectChoice(value: number): void {
    this.choices[value].isCorrect = true;
  }

  changeCorrectChoice(event): void {
    this.choices.forEach(item => {
        if (item.id === event.value) {
            item.isCorrect = true;
            return;
        }
        item.isCorrect = false;
    });
    this.initCheck();
  }

  initCheck(): void {
    const check = this.choices.some(c => c.isCorrect === true);
    this.attemptService.changeInfoChoice(this.initInfoChoice(check));
  }

  initInfoChoice(isCheck: boolean): InfoChoice {
    const infoChoice = new InfoChoice();
    infoChoice.questionId = this.question.id;
    infoChoice.choices = this.choices;
    infoChoice.cssclass = this.getCssQuestionNav(isCheck);
    return infoChoice;
  }

  getCssQuestionNav(isCheck: boolean): object {
    return {
      'question-border-check': isCheck,
      'question-border-uncheck': !isCheck
    };
  }

  initRandom(): void {
      if (this.question.type === this.questionType.TrueFalse) {
        return;
      }
      if (this.settings.randomize) {
        this.shuffleArray();
      }
  }

  shuffleArray(): void {
      this.choices.sort(() => Math.random() - 0.5);
  }
}
