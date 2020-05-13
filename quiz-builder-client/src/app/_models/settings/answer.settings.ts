export class BaseChoiceSettings {
  choicesDisplayType = ChoicesDisplayType.Dropdown;
  choicesEnumerationType = ChoicesEnumerationType.one_two_three;
  randomize = false;
}

export class SettingsTrueFalse extends BaseChoiceSettings {
}

export class SettingsMultipleSelectQuestion extends BaseChoiceSettings {
  gradingType = QuestionGradingType.AllOrNothing;
}

export class SettingsMultipleChoiceQuestion extends BaseChoiceSettings {
}

export class DefaultEnumChoice {
  one_two_three = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
  i_ii_iii = ['i', 'ii', 'iii', 'iv', 'v', 'vi', 'vii', 'viii', 'ix', 'x'];
  I_II_III = ['I', 'II', 'III', 'IV', 'V', 'VI', 'VII', 'VIII', 'IX', 'X'];
  a_b_c = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j'];
  A_B_C = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'];
}

export enum ChoicesDisplayType {
  None = 0,
  Horizontal = 1,
  Vertical = 2,
  Dropdown = 3
}

export enum ChoicesEnumerationType {
  None = 0,
  one_two_three = 1,
  i_ii_iii = 2,
  I_II_III = 3,
  a_b_c = 4,
  A_B_C = 5,
  NoEnumeration = 6
}

export enum QuestionGradingType {
  None = 0,
  AllOrNothing = 1,
  RightMinusWrong = 2,
  CorrectAnswers = 3
}
