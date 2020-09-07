import { Injectable } from '@angular/core';
import { OptionItem } from './optionItem';
import { QuestionType } from '../question';

@Injectable({
    providedIn: 'root'
})

export class OptionItemsService {

    getQuestionTypeOptionItems(questionType: QuestionType) {
        switch (+questionType) {
            case QuestionType.TrueFalse: return [
                new OptionItem('name', 'Name', '', false),
                new OptionItem('correctFeedback', 'Correct Feedback', 'wysiwyg', false),
                new OptionItem('incorrectFeedback', 'Incorrect Feedback', 'wysiwyg', false),
                new OptionItem('questionDisplayType', 'Display Type', '', false),
            ];
            case QuestionType.MultipleChoice: return [
                new OptionItem('name', 'Name', '', false),
                new OptionItem('correctFeedback', 'Correct Feedback', 'wysiwyg', false),
                new OptionItem('incorrectFeedback', 'Incorrect Feedback', 'wysiwyg', false),
                new OptionItem('questionDisplayType', 'Display Type', '', false),
            ];
            case QuestionType.MultiSelect: return [
                new OptionItem('name', 'Name', '', false),
                new OptionItem('correctFeedback', 'Correct Feedback', 'wysiwyg', false),
                new OptionItem('incorrectFeedback', 'Incorrect Feedback', 'wysiwyg', false),
                new OptionItem('questionDisplayType', 'Display Type', '', false),
                new OptionItem('questionGradingType', 'Grading Type', '', false),
            ];
            case QuestionType.LongAnswer: return [
                new OptionItem('name', 'Name', '', false)
            ];
            case QuestionType.Empty: return [];
        }
    }

}
