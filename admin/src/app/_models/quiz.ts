import { Group } from './group';

export class Quiz {
    id: string;
    name: string;
    isEnabled: boolean;
    groups: Group[];
}
