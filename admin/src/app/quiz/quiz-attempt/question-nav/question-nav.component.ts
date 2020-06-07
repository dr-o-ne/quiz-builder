import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Question } from 'src/app/_models/question';
import { InfoChoice } from 'src/app/_models/option';
import { ActivatedRoute } from '@angular/router';
import { AttemptService } from 'src/app/_service/attempt.service';
import { ChoiceSelection, QuestionAttempt, QuizAttempt } from 'src/app/_models/attempt';
import { Choice } from 'src/app/_models/choice';

@Component({
  selector: 'app-question-nav',
  templateUrl: './question-nav.component.html',
  styleUrls: ['./question-nav.component.css']
})
export class QuestionNavComponent implements OnInit {
  @Input() questions: Question[];
  @Input() anchor: string;
  @Input() quizAttemptId: string;

  @Output() updateQuizAttempt = new EventEmitter<QuizAttempt>();

  cssQuestionNav = {
    'question-border-check': false,
    'question-border-uncheck': true
  };

  infoChoiceArr: InfoChoice[];

  constructor(
    private attemptService: AttemptService,
    private activeRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.initInfoChoiceArr();
    this.routeToFragment();
    this.subscribeChangeInfoChoice();
  }

  routeToFragment(): void {
    this.activeRoute.fragment.subscribe((fragment: string) => {
      const elem = document.getElementById(fragment);
      if ( fragment && elem ) {
        elem.scrollIntoView({behavior: 'auto'});
      }
    });
  }

  subscribeChangeInfoChoice(): void {
    this.attemptService.currentInfoChoice.subscribe((infoChoice: InfoChoice) => {
      if ( !infoChoice ) {
        return;
      }
      this.currentQuestionPassed(infoChoice);
    });
  }

  initInfoChoiceArr(): void {
    this.infoChoiceArr = [];
    if  (!this.questions ) {
      return;
    }
    this.questions.forEach(q => {
      const infoChoice = new InfoChoice();
      infoChoice.questionId = q.id;
      infoChoice.cssclass = this.cssQuestionNav;
      this.infoChoiceArr.push(infoChoice);
    });
  }

  currentQuestionPassed(infoChoice: InfoChoice): void {
    if ( !infoChoice.questionId ) {
      return;
    }
    this.infoChoiceArr.some(item => {
      if (item.questionId === infoChoice.questionId) {
        item.cssclass = infoChoice.cssclass;
        return true;
      }
    });
  }

  clickSubmit(): void {
    this.updateQuizAttempt.emit(this.getQuizAttempt());
  }

  getQuizAttempt(): QuizAttempt {
    const questionAttemptArr = [];
    this.questions.forEach(question => {
      const choiceSelectionArr = [];
      question.choices.forEach(choice => {
        if (choice.isCorrect) {
          choiceSelectionArr.push(this.getChoiceSelection(choice));
        }
      });
      questionAttemptArr.push(this.getQuestionAttempt(question.id, choiceSelectionArr));
    });
    return new QuizAttempt(this.quizAttemptId, questionAttemptArr);
  }

  getChoiceSelection(choice: Choice): ChoiceSelection {
    return new ChoiceSelection(choice.id, choice.isCorrect);
  }

  getQuestionAttempt(questionId: string, choiceSelection: ChoiceSelection[]): QuestionAttempt {
    return new QuestionAttempt(questionId, choiceSelection);
  }

  validSubmit(): boolean {
    let isValid = false;
    this.questions.some(question => {
      question.choices.some(choice => {
        isValid = choice.isCorrect === true;
        return isValid;
      });
      return isValid;
    });
    return !isValid;
  }

  clickNav(anchor: string) {
    const elem = document.getElementById(anchor);
    elem?.scrollIntoView({behavior: 'smooth'});
  }

}
