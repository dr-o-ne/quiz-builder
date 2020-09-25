import { Injectable } from '@angular/core';
import { ChoicesDisplayType, ChoicesEnumerationType, QuestionGradingType } from 'app/quiz-builder/model/settings/answer.settings';

@Injectable({
    providedIn: 'root'
})
export class ChoiceLangService {

    getChoiceEnumerationTypeLangTerm(input: ChoicesEnumerationType): string {

        switch (+input) {
            case ChoicesEnumerationType.NoEnumeration: return "Empty";
            case ChoicesEnumerationType.A_B_C: return "A B C";
            case ChoicesEnumerationType.a_b_c: return "a b c";
            case ChoicesEnumerationType.i_ii_iii: return "i ii iii";
            case ChoicesEnumerationType.I_II_III: return "I II III";
            case ChoicesEnumerationType.one_two_three: return "1 2 3";
        }

    }

    getChoiceDisplayTypeLangTerm(input: ChoicesDisplayType): string {

        switch (+input) {
            case ChoicesDisplayType.Vertical: return "Vertical";
            case ChoicesDisplayType.Horizontal: return "Horizontal";
            case ChoicesDisplayType.Dropdown: return "Dropdown";
        }

    }

    getChoiceGradingTypeLangTerm(input: QuestionGradingType): string {

        switch (+input) {
            case QuestionGradingType.AllOrNothing: return "All or Nothing";
            case QuestionGradingType.CorrectAnswers: return "Correct Only";
            case QuestionGradingType.RightMinusWrong: return "Right - Wrong";
        }

    }

}
