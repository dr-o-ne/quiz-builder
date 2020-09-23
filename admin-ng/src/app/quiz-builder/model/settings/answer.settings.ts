export class DefaultEnumChoice {
  one_two_three = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
  i_ii_iii = ['i', 'ii', 'iii', 'iv', 'v', 'vi', 'vii', 'viii', 'ix', 'x'];
  I_II_III = ['I', 'II', 'III', 'IV', 'V', 'VI', 'VII', 'VIII', 'IX', 'X'];
  a_b_c = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j'];
  A_B_C = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'];
}

export enum ChoicesDisplayType {
  Horizontal = 1,
  Vertical = 2,
  Dropdown = 3
}

export enum ChoicesEnumerationType {
  NoEnumeration = 6,
  one_two_three = 1,
  i_ii_iii = 2,
  I_II_III = 3,
  a_b_c = 4,
  A_B_C = 5,
}

export enum QuestionGradingType {
  AllOrNothing = 1,
  RightMinusWrong = 2,
  CorrectAnswers = 3
}
