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
