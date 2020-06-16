import { Appearance } from './appearance';
import { ChoicesDisplayType } from './_enums';

export class QuizAttemptInfo {
    id: string;
    appearance: Appearance;
    name: string;
    groups: GroupAttemptInfo[]
}

export class GroupAttemptInfo {
    id: string;
    questions: QuestionAttemptInfo[]
}

export type QuestionAttemptInfo = 
    TrueFalseQuestionAttemptInfo | 
    MultipleChoiceQuestionAttemptInfo |
    MultipleSelectQuestionAttemptInfo;

export class TrueFalseQuestionAttemptInfo {
    id: string;
    text: string;
    isHtmlText: boolean;
    type: number;
    choices: BinaryChoiceAttemptInfo[];
    choicesDisplayType: ChoicesDisplayType;
}

export class MultipleChoiceQuestionAttemptInfo {
    id: string;
    text: string;
    isHtmlText: boolean;
    type: number;
    choices: BinaryChoiceAttemptInfo[];
    choicesDisplayType: ChoicesDisplayType;
}

export class MultipleSelectQuestionAttemptInfo {
    id: string;
    text: string;
    isHtmlText: boolean;
    type: number;
    choices: BinaryChoiceAttemptInfo[];
    choicesDisplayType: ChoicesDisplayType;
}

export class BinaryChoiceAttemptInfo {
    id: number;
    text: string;
}