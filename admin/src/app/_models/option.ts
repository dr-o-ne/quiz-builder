import { Choice } from './choice';

export class Option {
  constructor(
    public name: string,
    public displayName: string,
    public type: string,
    public enabled: boolean = false,
  ) {}
}

export class InfoChoice {
    public questionId: string;
    public choices?: Choice[];
    public cssclass: any;
}
