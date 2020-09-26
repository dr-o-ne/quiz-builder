import { Group } from './group';

export class Quiz {
    id: string;
    name: string;
    isEnabled: boolean;
    groups: Group[] = [];

    isIntroductionEnabled: boolean;
    introduction: string;

    isPrevButtonEnabled: boolean;
    pageSettings: PageSettings;
    questionsPerPage: number;
    randomizeGroups: boolean;
    randomizeQuestions: boolean;

    isScheduleEnabled: boolean;
    startDate: number;
    endDate: number;
}

export enum PageSettings {
    PagePerGroup = 1,
    PagePerQuiz = 2,
    PagePerQuestion = 3,
    Custom = 4
}