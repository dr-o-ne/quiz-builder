import { Question } from './question';

export class Quiz {
    id: number;
    name?: string;
    status?: string;
    question?: Question[];
}
