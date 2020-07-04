import { Question } from './question';

export class Group {

  id: string;
  quizId: string;
  name: string;
  questions: Question[] = new Array<Question>();

}
