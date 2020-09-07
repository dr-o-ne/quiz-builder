import { Group } from './group';

export enum PageSettings {
    PagePerGroup = 1,
    PagePerQuiz = 2,
    PagePerQuestion = 3,
    Custom = 4
}

export class Quiz {
    id: string;
    name: string;
    isEnabled: boolean;
    groups: Group[];

    introduction: string;

    isPrevButtonEnabled: boolean;
    pageSettings: PageSettings;
    questionsPerPage: number;
    randomizeGroups: boolean;
    randomizeQuestions: boolean;

    isScheduleEnabled: boolean;
    startDate: number;
    endDate: number;

    constructor() {
        this.groups = new Array<Group>();
    }
}
