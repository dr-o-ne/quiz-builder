import {QuestionType} from './_enums';

export class AttemptInfo {
    id: string;
    quiz: QuizAttemptInfo; 
}

export class QuizAttemptInfo {
    id: string;
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