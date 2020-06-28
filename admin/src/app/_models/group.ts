import { Question } from './question';

export class Group {

  id: string;
  quizId: string;
  name: string = 'Default';
  questions: Question[] = new Array<Question>();

}
