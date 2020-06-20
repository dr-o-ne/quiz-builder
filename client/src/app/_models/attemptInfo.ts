import { Appearance } from './appearance';
import { ChoicesDisplayType } from './_enums';

export interface QuizAttemptInfo {
    id: string;
    appearance: Appearance;
    name: string;
    groups: GroupAttemptInfo[]
}

export interface GroupAttemptInfo {
    id: string;
    questions: QuestionAttemptInfo[]
}

export type QuestionAttemptInfo =
    TrueFalseQuestionAttemptInfo |
    MultipleChoiceQuestionAttemptInfo |
    MultipleSelectQuestionAttemptInfo |
    LongAnswerQuestionAttemptInfo

export interface TrueFalseQuestionAttemptInfo {
    id: string;
    text: string;
    isHtmlText: boolean;
    type: number;
    choices: BinaryChoiceAttemptInfo[];
    choicesDisplayType: ChoicesDisplayType;
}

export interface MultipleChoiceQuestionAttemptInfo {
    id: string;
    text: string;
    isHtmlText: boolean;
    type: number;
    choices: BinaryChoiceAttemptInfo[];
    choicesDisplayType: ChoicesDisplayType;
}

export interface MultipleSelectQuestionAttemptInfo {
    id: string;
    text: string;
    isHtmlText: boolean;
    type: number;
    choices: BinaryChoiceAttemptInfo[];
    choicesDisplayType: ChoicesDisplayType;
}

export interface LongAnswerQuestionAttemptInfo {
    id: string;
    text: string;
    isHtmlText: boolean;
    type: number;
}

export interface BinaryChoiceAttemptInfo {
    id: number;
    text: string;
}