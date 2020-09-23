import { Injectable } from '@angular/core';
import { PageSettings } from 'app/quiz-builder/model/quiz';

@Injectable({
    providedIn: 'root'
})
export class QuizLangService {

    getPageSettingsLangTerm(input: PageSettings) { //TODO: review langterms
        switch (+input) {
            case PageSettings.PagePerGroup: return "Page per Group";
            case PageSettings.PagePerQuiz: return "Page per Quiz";
            case PageSettings.PagePerQuestion: return "Each Question on new Page";
            case PageSettings.Custom: return "Questions per Page";
        }
    }

}
