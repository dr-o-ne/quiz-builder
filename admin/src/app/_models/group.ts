import { Question } from './question';

export class Group {

  questions: Question[];

  constructor(
    public id: string,
    public quizId: string,
    public name: string = 'Default'
  ) {
    this.questions = [];
  }
}
