import { Question } from './question';
import { Group } from './group';

export class Quiz {
    id: number;
    name: string;
    status?: string;
    groups?: Group[];
}
