import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Quiz } from '../_models/quiz';
import { QuestionService } from '../_service/question.service';
import { AnswerService } from '../_service/answer.service';
import { Question } from '../_models/question';
import { Answer } from '../_models/answer';

@Component({
  selector: 'app-preview-quiz',
  templateUrl: './preview-quiz.component.html',
  styleUrls: ['./preview-quiz.component.css']
})
export class PreviewQuizComponent implements OnInit {
  quiz: Quiz;
  questionList: Question[];
  answerList: Answer[];

  selectedAnswer: string;

  constructor(private router: Router, private activeRout: ActivatedRoute,
              private questionService: QuestionService, private answerService: AnswerService) {}

  ngOnInit() {
    this.activeRout.data.subscribe(response => {
      if (response.hasOwnProperty('quizResolver')) {
        this.quiz = response.quizResolver.quiz;
        this.initPreview();
      } else {
        console.log('Not found correct quiz');
      }
    });
  }

  initPreview() {
    const storageQuestion = localStorage.getItem('questionlist');
    const storageAnswer = localStorage.getItem('answerlist');
    if (!storageQuestion && !storageAnswer) {
      this.questionService.getQuestionData().subscribe((data: any) => {
        this.questionList = data.questionlist;
        this.answerService.getAnswerData().subscribe((ans: any) => {
            this.answerList = ans.answerlist;
            this.buildStructQuiz();
          }, error => console.log(error));
      }, error => console.log(error));
      return;
    }
    if (storageQuestion && !storageAnswer) {
      this.questionList = JSON.parse(storageQuestion);
      this.answerService.getAnswerData().subscribe((ans: any) => {
        this.answerList = ans.answerlist;
        this.buildStructQuiz();
      }, error => console.log(error));
      return;
    }
    if (storageQuestion && storageAnswer) {
      this.questionList = JSON.parse(storageQuestion);
      this.answerList = JSON.parse(storageAnswer);
      this.buildStructQuiz();
      return;
    }
  }

  buildStructQuiz() {
    this.questionList = this.questionList.filter(item => item.quizId === this.quiz.id);
    this.questionList.forEach(item => {
      item.answers = this.answerList.filter(ans => ans.questionId === item.id).map(changeItem => {
        return {
          id: changeItem.id,
          isCorrect: false,
          name: changeItem.name,
          questionId: changeItem.questionId
        };
      });
    });
  }

  changeRadioButton(event) {
  }

}
