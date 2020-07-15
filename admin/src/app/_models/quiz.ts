import { Group } from './group';

export class Quiz {
    id: string;
    name: string;
    isEnabled: boolean;
    groups: Group[];

    isPrevButtonEnabled: boolean;

    constructor() {
        this.groups = new Array<Group>();
    }
}
