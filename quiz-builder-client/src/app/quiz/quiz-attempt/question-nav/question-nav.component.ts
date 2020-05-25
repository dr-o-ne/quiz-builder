import { Component, OnInit, Input } from '@angular/core';
import { Question } from 'src/app/_models/question';
import { InfoChoice } from 'src/app/_models/option';
import { ActivatedRoute } from '@angular/router';
import { AttemptService } from 'src/app/_service/attempt.service';

@Component({
  selector: 'app-question-nav',
  templateUrl: './question-nav.component.html',
  styleUrls: ['./question-nav.component.css']
})
export class QuestionNavComponent implements OnInit {
  @Input() questions: Question[];
  @Input() anchor: string;

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
        elem.scrollIntoView();
      }
    });
  }

  subscribeChangeInfoChoice(): void {
    this.attemptService.currentInfoChoice.subscribe((infoChoice: InfoChoice) => {
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

}
