export class QuizAttemptResult {
    id: string;
    quizId: string;
}

export type QuestionAttemptResult = 
    TrueFalseQuestionAttemptResult |
    MultipleChoiceQuestionAttemptInfo |
    MultipleSelectQuestionAttemptInfo;

export class TrueFalseQuestionAttemptResult {
    id: string;
    choices: BinaryChoiceAttemptResult;
}

export class MultipleChoiceQuestionAttemptInfo {
    id: string;
    choices: BinaryChoiceAttemptResult;
}

export class MultipleSelectQuestionAttemptInfo {
    id: string;
    choices: BinaryChoiceAttemptResult[];
}

export class BinaryChoiceAttemptResult {
    id: number;
    isSelected: boolean;
}