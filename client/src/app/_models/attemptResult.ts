export class QuizAttemptResult {
    id: string;
    answers: QuestionAttemptResult[];
}

export type QuestionAttemptResult =
    TrueFalseQuestionAttemptResult |
    MultipleChoiceQuestionAttemptResult |
    MultipleSelectQuestionAttemptResult |
    LongAnswerQuestionAttemptResult;

export class TrueFalseQuestionAttemptResult {
    questionId: string;
    choice: number;

    isCompleted(): boolean {
        return this.choice >= 0;
    }
}

export class MultipleChoiceQuestionAttemptResult {
    questionId: string;
    choice: number;

    isCompleted(): boolean {
        return this.choice >= 0;
    }
}

export class MultipleSelectQuestionAttemptResult {
    questionId: string;
    choices: number[];

    isCompleted(): boolean {
        return Array.isArray(this.choices) && this.choices.length > 0;
    }
}

export class LongAnswerQuestionAttemptResult {
    questionId: string;
    text: string;

    isCompleted(): boolean {
        return this.text !== "";
    }
}