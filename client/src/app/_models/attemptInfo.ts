import { QuestionType } from './_enums';
import { Appearance } from './appearance';

export class AttemptInfo {
    id: string;
    appearance: Appearance;
    quiz: QuizAttemptInfo; 
}

export class QuizAttemptInfo {
    id: string;
    name: string;
    groups: GroupAttemptInfo[]
}

export class GroupAttemptInfo {
    id: string;
    questions: QuestionAttemptInfo[]
}

export class QuestionAttemptInfo {
    id: string;
    type: QuestionType;
    choices: ChoiceAttemptInfo[]
}

export class ChoiceAttemptInfo {
    id: number;
    text: string;
}

