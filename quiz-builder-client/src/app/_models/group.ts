import { Question } from './question';

export class Group {
    id: number;
    name: string;
    quizId: number;
    question?: Question[];
}
