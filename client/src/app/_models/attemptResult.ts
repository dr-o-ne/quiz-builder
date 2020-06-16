export class QuizAttemptResult {
    id: string;
    answers: QuestionAttemptResult[];
}

export type QuestionAttemptResult = 
    TrueFalseQuestionAttemptResult |
    MultipleChoiceQuestionAttemptResult |
    MultipleSelectQuestionAttemptResult;

export class TrueFalseQuestionAttemptResult {
    questionId: string;
    choice: number;
}

export class MultipleChoiceQuestionAttemptResult {
    questionId: string;
    choice: number;
}

export class MultipleSelectQuestionAttemptResult {
    questionId: string;
    choices: number[];
}