export class QuizAttemptResult {

    id: string;
    answers: QuestionAttemptResult[];

    constructor(id: string, answers: QuestionAttemptResult[]) {
        this.id = id;
        this.answers = answers;
    }

}

export type QuestionAttemptResult =
    TrueFalseQuestionAttemptResult |
    MultipleChoiceQuestionAttemptResult |
    MultipleSelectQuestionAttemptResult |
    LongAnswerQuestionAttemptResult;

export class TrueFalseQuestionAttemptResult {
    
    questionId: string;
    choice: number;

    constructor(questionId: string, choice: number) {
        this.questionId = questionId;
        this.choice = choice;
    }

    isCompleted(): boolean {
        return this.choice >= 0;
    }
}

export class MultipleChoiceQuestionAttemptResult {
    questionId: string;
    choice: number;

    constructor(questionId: string, choice: number) {
        this.questionId = questionId;
        this.choice = choice;
    }

    isCompleted(): boolean {
        return this.choice >= 0;
    }
}

export class MultipleSelectQuestionAttemptResult {
    questionId: string;
    choices: number[];

    constructor(questionId: string, choices: number[]) {
        this.questionId = questionId;
        this.choices = choices;
    }

    isCompleted(): boolean {
        return Array.isArray(this.choices) && this.choices.length > 0;
    }
}

export class LongAnswerQuestionAttemptResult {
    questionId: string;
    text: string;

    constructor(questionId: string, text: string) {
        this.questionId = questionId;
        this.text = text;
    }

    isCompleted(): boolean {
        return this.text !== "";
    }
}