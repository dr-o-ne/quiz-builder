export class QuizAttemptResult {
    id: string;
    quizId: string;
}

export type QuestionAttemptResult = 
    TrueFalseQuestionAttemptResult |
    MultipleChoiceQuestionAttemptResult |
    MultipleSelectQuestionAttemptResult;

export class TrueFalseQuestionAttemptResult {
    id: string;
    choices: BinaryChoiceAttemptResult;
}

export class MultipleChoiceQuestionAttemptResult {
    id: string;
    choices: BinaryChoiceAttemptResult;
}

export class MultipleSelectQuestionAttemptResult {
    id: string;
    choices: BinaryChoiceAttemptResult[];
}

export class BinaryChoiceAttemptResult {
    id: number;
    isSelected: boolean;
}