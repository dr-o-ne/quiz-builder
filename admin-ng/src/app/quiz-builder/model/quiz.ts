import { Group } from './group';

export class Quiz {
    id: string;
    name: string;
    isEnabled: boolean;
    groups: Group[] = [];

    description: string;

    isPrevButtonEnabled: boolean;
    pageSettings: PageSettings;
    questionsPerPage: number;
    randomizeGroups: boolean;
    randomizeQuestions: boolean;
    isPassingScoreEnabled: boolean;
    passingScore: number;

    isScheduleEnabled: boolean;
    startDate: number;
    endDate: number;

    headerColor: string;
    backgroundColor: string;
    sideColor: string;
    footerColor: string;

    isStartPageEnabled: boolean;
    isTotalAttemptsEnabled: boolean;
    isTimeLimitEnabled: boolean;
    isTotalQuestionsEnabled: boolean;
    isPassingScoreWidgetEnabled: boolean;

    resultPassText: string;
    resultFailText: string;
    isResultPageEnabled: boolean;
    isResultTotalScoreEnabled: boolean;
    isResultPassFailEnabled: boolean;
    isResultFeedbackEnabled: boolean;
    isResultDurationEnabled: boolean;
}

export enum PageSettings {
    PagePerGroup = 1,
    PagePerQuiz = 2,
    PagePerQuestion = 3,
    Custom = 4
}