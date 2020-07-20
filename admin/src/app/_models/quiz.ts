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

    isPrevButtonEnabled: boolean;
    pageSettings: PageSettings;
    questionsPerPage: number;

    constructor() {
        this.groups = new Array<Group>();
        this.questionsPerPage = 5;        
        this.pageSettings = PageSettings.PagePerGroup;
    }
}
