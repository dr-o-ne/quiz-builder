import { Injectable } from '@angular/core';
import { QuestionType } from 'src/app/_models/question';
import { OptionItem } from './optionItem';

@Injectable({
    providedIn: 'root'
})

export class OptionItemsService {

    getQuestionTypeOptionItems(questionType: QuestionType) {
        switch (+questionType) {
            case QuestionType.TrueFalse: return [
                new OptionItem('correctFeedback', 'Correct Feedback', 'wysiwyg', false),
                new OptionItem('incorrectFeedback', 'Incorrect Feedback', 'wysiwyg', false),
                new OptionItem('questionDisplayType', 'Display Type', '', false),
            ];
            case QuestionType.MultipleChoice: return [
                new OptionItem('correctFeedback', 'Correct Feedback', 'wysiwyg', false),
                new OptionItem('incorrectFeedback', 'Incorrect Feedback', 'wysiwyg', false),
                new OptionItem('questionDisplayType', 'Display Type', '', false),
            ];
            case QuestionType.MultiSelect: return [
                new OptionItem('correctFeedback', 'Correct Feedback', 'wysiwyg', false),
                new OptionItem('incorrectFeedback', 'Incorrect Feedback', 'wysiwyg', false),
                new OptionItem('questionDisplayType', 'Display Type', '', false),
            ];
            case QuestionType.LongAnswer: return [];
            case QuestionType.Empty: return [];
        }
    }

}
