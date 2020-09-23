import { Injectable } from '@angular/core';
import { QuestionType } from 'app/quiz-builder/model/question';

@Injectable({
    providedIn: 'root'
})
export class QuestionLangService {

    getQuestionTypeLangTerm(questionType: QuestionType) {

        console.log(questionType);


        switch (+questionType) {
            case QuestionType.TrueFalse: return "True False";
            case QuestionType.MultipleChoice: return "Multiple Choice";
            case QuestionType.MultiSelect: return "Multiple Select";
            case QuestionType.LongAnswer: return "Long Answer";
            case QuestionType.Empty: return "Text Block";
        }
    }

}
