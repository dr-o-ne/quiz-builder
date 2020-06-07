export class ChoiceSelection {
    constructor(
        public id: number,
        public isSelected: boolean
    ) {}
}

export class QuestionAttempt {
    constructor(
        public questionId: string,
        public binaryChoiceSelections?: ChoiceSelection[]
    ) {}
}

export class QuizAttempt {
    constructor(
        public id: string,
        public questionAnswers: QuestionAttempt[]
    ) {}
}
