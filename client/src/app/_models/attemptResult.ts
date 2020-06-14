export class QuizAttemptResult {
    id: string;
    quizId: string;
}

export class QuestionAttemptResult {
    id: string;
    questionId: string;
}

export class ChoiceAttemptResult {
    id: number;
    text: string;
}

export class BinaryChoiceAttemptResult {
    id: number;
    isSelected: boolean;
}