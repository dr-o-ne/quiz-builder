import {QuestionType} from './_enums';

export class AttemptInfo {
    id: string;
    quiz: QuizAttemptInfo; 
}

export class QuizAttemptInfo {
    id: string;
}

export class QuestionAttemptInfo {
    id: string;
    type: QuestionType;
}

export class ChoiceAttemptInfo {
    id: number;
    text: string;
}