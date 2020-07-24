import { Question } from './question';

export class Group {

  id: string;
  quizId: string;
  name: string;
  selectAllQuestions: boolean;
  countOfQuestionsToSelect?: number;
  randomizeQuestions: boolean;
  questions: Question[] = new Array<Question>();

}
