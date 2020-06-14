import { Appearance } from './appearance';
import { ChoicesDisplayType } from './_enums';

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

export type QuestionAttemptInfo = TrueFalseQuestionAttemptInfo | MultipleChoiceQuestionAttemptInfo;

export class TrueFalseQuestionAttemptInfo {
    id: string;
    text: string;
    type: number;
    choices: BinaryChoiceAttemptInfo[];
    choicesDisplayType: ChoicesDisplayType;
    setting1: boolean;
}

export class MultipleChoiceQuestionAttemptInfo {
    id: string;
    text: string;
    type: number;
    choices: BinaryChoiceAttemptInfo[];
    choicesDisplayType: ChoicesDisplayType;
    setting2: boolean;
}

export class BinaryChoiceAttemptInfo {
    id: number;
    text: string;
}