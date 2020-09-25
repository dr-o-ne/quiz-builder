export class Choice {
  constructor(
    public id: number,
    public text: string = '',
    public isCorrect: boolean = false,
    public feedback: string = '',
    public points: number = 0
  ) {
  }
}
