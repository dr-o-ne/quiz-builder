import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Quiz } from '../_models/quiz';
import { QuestionService } from '../_service/question.service';
import { AnswerService } from '../_service/answer.service';
import { Question } from '../_models/question';
import { Answer } from '../_models/answer';
import { Group } from '../_models/group';
import { QuizService } from '../_service/quiz.service';

@Component({
  selector: 'app-preview-quiz',
  templateUrl: './preview-quiz.component.html',
  styleUrls: ['./preview-quiz.component.css']
})
export class PreviewQuizComponent implements OnInit {
  quiz: Quiz;
  questionList: Question[];
  answerList: Answer[];
  groupList: Group[];

  currentGroup: Group;
  currentIndex = 0;

  selectedAnswer: string;

  constructor(private router: Router, private activeRout: ActivatedRoute,
              private questionService: QuestionService, private answerService: AnswerService,
              private quizService: QuizService) {}

  ngOnInit() {
    this.activeRout.data.subscribe(data => {
      if (data.hasOwnProperty('quiz')) {
        this.quiz = data.quiz;
        this.initPreview();
      } else {
        console.log('Not found correct quiz');
      }
    });
  }

  initPreview() {
    const storageQuestion = localStorage.getItem('questionlist');
    const storageAnswer = localStorage.getItem('answerlist');
    const storageGroup = localStorage.getItem('grouplist');
    if (!storageQuestion && !storageAnswer && !storageGroup) {
      this.quizService.getGroupData().subscribe((group: any) => {
        this.groupList = group.grouplist;
        this.questionService.getQuestionData().subscribe((question: any) => {
          this.questionList = question.questionlist;
          this.answerService.getAnswerData().subscribe((ans: any) => {
            this.answerList = ans.answerlist;
            this.buildStructQuiz();
          }, error => console.log(error));
        }, error => console.log(error));
      }, error => console.log(error));
      return;
    }
    if (storageGroup && !storageQuestion && !storageAnswer) {
      this.groupList = JSON.parse(storageGroup);
      this.questionService.getQuestionData().subscribe((question: any) => {
        this.questionList = question.questionlist;
        this.answerService.getAnswerData().subscribe((ans: any) => {
          this.answerList = ans.answerlist;
          this.buildStructQuiz();
        }, error => console.log(error));
      }, error => console.log(error));
      return;
    }
    if (storageGroup && storageQuestion && !storageAnswer) {
      this.groupList = JSON.parse(storageGroup);
      this.questionList = JSON.parse(storageQuestion);
      this.answerService.getAnswerData().subscribe((ans: any) => {
        this.answerList = ans.answerlist;
        this.buildStructQuiz();
      }, error => console.log(error));
    }
    if (storageQuestion && storageGroup && storageAnswer) {
      this.questionList = JSON.parse(storageQuestion);
      this.groupList = JSON.parse(storageGroup);
      this.answerList = JSON.parse(storageAnswer);
      this.buildStructQuiz();
      return;
    }
  }

  buildStructQuiz() {
    this.groupList = this.groupList.filter(item => item.quizId === this.quiz.id);
    this.groupList.forEach(group => {
      group.question = this.questionList.filter(question => question.groupId === group.id);
      group.question.forEach(question => {
        question.answers = this.answerList.filter(answer => answer.questionId === question.id).map(changeAns => {
          return {
            id: changeAns.id,
            isCorrect: false,
            name: changeAns.name,
            questionId: changeAns.questionId
          };
        });
      });
    });
    this.currentGroup = this.groupList[this.currentIndex];
  }

  changeRadioButton(event) {
  }

  nextPage() {
    if (this.groupList.length > this.currentIndex) {
      this.currentGroup = this.groupList[++this.currentIndex];
    }
  }

  prevPage() {
    this.currentGroup = this.groupList[--this.currentIndex];
  }

}
