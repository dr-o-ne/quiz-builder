import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Quiz } from 'src/app/_models/quiz';
import { QuestionService } from 'src/app/_service/question.service';
import { Question } from 'src/app/_models/question';
import { Choice } from 'src/app/_models/choice';
import { Group } from 'src/app/_models/group';
import { QuizService } from 'src/app/_service/quiz.service';

@Component( {
  selector: 'app-preview-quiz',
  templateUrl: './preview-quiz.component.html',
  styleUrls: [ './preview-quiz.component.css' ]
} )
export class PreviewQuizComponent implements OnInit {
  quiz: Quiz;
  questions: Question[];
  questionList: Question[];
  answerList: Choice[];
  groupList: Group[];

  currentGroup: Group;
  currentIndex = 0;

  selectedAnswer: string;

  constructor( private router: Router,
               private activeRoute: ActivatedRoute,
               private questionService: QuestionService,
               private quizService: QuizService
  ) {
  }

  ngOnInit() {
    if ( !history.state.quiz ) {
      this.router.navigate(['../../'], { relativeTo: this.activeRoute.parent });
      return;
    }
    this.quiz = history.state.quiz;

    this.activeRoute.data.subscribe( response => {
      if ( response.hasOwnProperty( 'quizResolver' ) ) {
        this.questions = response.quizResolver.questions;
        this.initPreview();
      } else {
        console.log( 'Not found correct quiz' );
      }
    } );
  }

  initPreview(): void {
    this.questions.every(q => q.choices = JSON.parse( q.choices ));
  }

  nextPage() {
    if ( this.groupList.length > this.currentIndex ) {
      this.currentGroup = this.groupList[++this.currentIndex];
    }
  }

  prevPage() {
    this.currentGroup = this.groupList[--this.currentIndex];
  }
}
