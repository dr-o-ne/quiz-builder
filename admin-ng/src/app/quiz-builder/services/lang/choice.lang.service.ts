import { Injectable } from '@angular/core';
import { ChoicesEnumerationType } from 'app/quiz-builder/model/settings/answer.settings';

@Injectable({
    providedIn: 'root'
})
export class ChoiceLangService {

    getChoiceTypeLangTerm(input: ChoicesEnumerationType) {

        switch (+input) {
            case ChoicesEnumerationType.NoEnumeration: return "Empty";
            case ChoicesEnumerationType.A_B_C: return "A B C";
            case ChoicesEnumerationType.a_b_c: return "a b c";
            case ChoicesEnumerationType.i_ii_iii: return "i ii iii";
            case ChoicesEnumerationType.I_II_III: return "I II III";
            case ChoicesEnumerationType.one_two_three: return "1 2 3";
        }


    }

}
