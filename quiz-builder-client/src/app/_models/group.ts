import { Question } from './question';

export class Group {
    id: string;
    name: string;
    quizId: string;
    question?: Question[];
}
