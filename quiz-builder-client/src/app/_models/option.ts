import { Choice } from './choice';

export class Option {
  constructor(
    public name: string,
    public displayName: string,
    public type: string,
    public enabled: boolean = false,
  ) {}
}

export class GroupForm {
  public name: string;
  public visible = true;
}

export type Action = () => void;

export class BtnGroupControl {
  constructor(
    public name: string,
    public action: Action,
    public visible: boolean = true
  ) {}
}

export class InfoChoice {
    public questionId: string;
    public choices?: Choice[];
    public cssclass: any;
}
