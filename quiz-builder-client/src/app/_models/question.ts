import { Answer } from './answer';

export class Question {
    id: number;
    name?: string;
    type?: string;
    quizId: number;
    groupId: number;
    answers?: Answer[];
}
