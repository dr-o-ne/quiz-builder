import { Group } from './group';

export class Quiz {
    id: string;
    name: string;
    isVisible: boolean;
    status?: string;
    groups?: Group[];
}
