import { Injectable } from '@angular/core';
import { OptionItem } from './optionItem';
import { QuestionType } from '../question';

@Injectable({
    providedIn: 'root'
})
export class OptionItemsService {

    public static OPTION_NAME = 'name';
    public static OPTION_QUESTION_CORRECT_FEEDBACK = 'questionCorrectFeedback';
    public static OPTION_QUESTION_INCORRECT_FEEDBACK = 'questionIncorrectFeedback';
    public static OPTION_QUESTION_CHOICES_FEEDBACK = 'questionChoicesFeedback';
    public static OPTION_QUESTION_DISPLAY_TYPE = 'questionDisplayType';
    public static OPTION_QUESTION_GRADING_TYPE = 'questionGradingType';
    public static OPTION_QUESTION_ENUMERATION_TYPE = 'questionEnumerationType';


    getQuestionTypeOptionItems(questionType: QuestionType) {
        switch (+questionType) {
            case QuestionType.TrueFalse: return [
                new OptionItem(OptionItemsService.OPTION_NAME, 'Short Description', true),
                new OptionItem(OptionItemsService.OPTION_QUESTION_CORRECT_FEEDBACK, 'Correct Feedback', false),
                new OptionItem(OptionItemsService.OPTION_QUESTION_INCORRECT_FEEDBACK, 'Incorrect Feedback', false),
                new OptionItem(OptionItemsService.OPTION_QUESTION_DISPLAY_TYPE, 'Display Type', false),
                new OptionItem(OptionItemsService.OPTION_QUESTION_ENUMERATION_TYPE, 'Enumeration Type', false),
            ];
            case QuestionType.MultipleChoice: return [
                new OptionItem(OptionItemsService.OPTION_NAME, 'Short Description', true),
                new OptionItem(OptionItemsService.OPTION_QUESTION_CORRECT_FEEDBACK, 'Correct Feedback', false),
                new OptionItem(OptionItemsService.OPTION_QUESTION_INCORRECT_FEEDBACK, 'Incorrect Feedback', false),
                new OptionItem(OptionItemsService.OPTION_QUESTION_CHOICES_FEEDBACK, 'Choices Feedback', false),
                new OptionItem(OptionItemsService.OPTION_QUESTION_DISPLAY_TYPE, 'Display Type', false),
                new OptionItem(OptionItemsService.OPTION_QUESTION_ENUMERATION_TYPE, 'Enumeration Type', false),
            ];
            case QuestionType.MultiSelect: return [
                new OptionItem(OptionItemsService.OPTION_NAME, 'Short Description', true),
                new OptionItem(OptionItemsService.OPTION_QUESTION_CORRECT_FEEDBACK, 'Correct Feedback', false),
                new OptionItem(OptionItemsService.OPTION_QUESTION_INCORRECT_FEEDBACK, 'Incorrect Feedback', false),
                new OptionItem(OptionItemsService.OPTION_QUESTION_CHOICES_FEEDBACK, 'Choices Feedback', false),
                new OptionItem(OptionItemsService.OPTION_QUESTION_DISPLAY_TYPE, 'Display Type', false),
                new OptionItem(OptionItemsService.OPTION_QUESTION_ENUMERATION_TYPE, 'Enumeration Type', false),
                new OptionItem(OptionItemsService.OPTION_QUESTION_GRADING_TYPE, 'Grading Type', false),
            ];
            case QuestionType.LongAnswer: return [
                new OptionItem(OptionItemsService.OPTION_NAME, 'Short Description', true)
            ];
            case QuestionType.Empty: return [
                new OptionItem(OptionItemsService.OPTION_NAME, 'Short Description', true),
            ];
        }
    }

}
