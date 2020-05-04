import { Group } from './group';

export class Quiz {
    id: number;
    name: string;
    isVisible: boolean;
    status?: string;
    groups?: Group[];
}
