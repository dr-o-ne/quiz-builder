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
  public isHide = false;
}

export type Action = () => void;

export class BtnGroupControl {
  constructor(
    public name: string,
    public action: Action,
    public isHide: boolean = false
  ) {}
}
