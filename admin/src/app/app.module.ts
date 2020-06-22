import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './_material/material.module';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { AuthGuard } from './_guards/auth.guard';
import { QuizListComponent } from './quiz/quiz-list/quiz-list.component';
import { QuizService } from './_service/quiz.service';
import { QuizResolver } from './_resolvers/quiz.resolver';
import { QuestionService } from './_service/question.service';
import { QuizPageComponent } from './quiz/quiz-page/quiz-page.component';
import { QuestionPageComponent } from './question/question-page/question-page.component';
import { QuestionResolver } from './_resolvers/question.resolver';
import { QuestionListComponent } from './question/question-list/question-list.component';
import { TrueFalseAnswerComponent } from './question/answer/true-false-answer/true-false-answer.component';
import { MultipleChoiceAnswerComponent } from './question/answer/multiple-choice-answer/multiple-choice-answer.component';
import { MultiSelectChoiceComponent } from './question/answer/multi-select-choice/multi-select-choice.component';
import { SingleChoiceDropdownAnswerComponent } from './question/answer/single-choice-dropdown-answer/single-choice-dropdown-answer.component';
import { MatNativeDateModule } from '@angular/material/core';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ChoiceDirective } from './_directive/choice.directive';
import { DynamicChoiceComponent } from './question/answer/base-choice/dynamic-choice.component';
import { ModalWindowPreviewQuestionComponent } from './question/modal-window/modal-window-preview-question.component';
import { QuestionPreviewDirective } from './_directive/question.preview.directive';
import { DynamicQuestionPreviewComponent } from './question/question-preview/base-question-preview/dynamic-question-preview.component';
import { TrueFalsePreviewComponent } from './question/question-preview/true-false-preview/true-false-preview.component';
import { MultiSelectPreviewComponent } from './question/question-preview/multi-select-preview/multi-select-preview.component';
import { LongAnswerComponent } from './question/answer/long-answer/long-answer.component';
import { LongAnswerPreviewComponent } from './question/question-preview/long-answer-preview/long-answer-preview.component';
import { AttemptService } from './_service/attempt.service';

import { AppRoutingModule } from './app-routing.module';
import { VexModule } from '../@vex/vex.module';
import { CustomLayoutModule } from './custom-layout/custom-layout.module';
import { CommingSoonComponent } from './common/comming-soon/comming-soon.component';

import { PageLayoutModule } from '../@vex/components/page-layout/page-layout.module';
import { SecondaryToolbarModule } from '../@vex/components/secondary-toolbar/secondary-toolbar.module';
import { MatButtonModule } from '@angular/material/button';
import { FlexLayoutModule } from '@angular/flex-layout';
import { BreadcrumbsModule } from '../@vex/components/breadcrumbs/breadcrumbs.module';
import { IconModule } from '@visurel/iconify-angular';
import { MatIconModule } from '@angular/material/icon';
import { ContainerModule } from '../@vex/directives/container/container.module';
import { QuillModule } from 'ngx-quill';

@NgModule( {
  declarations: [
    AppComponent,
    QuizListComponent,
    QuizPageComponent,
    QuestionPageComponent,
    QuestionListComponent,
    TrueFalseAnswerComponent,
    MultipleChoiceAnswerComponent,
    MultiSelectChoiceComponent,
    SingleChoiceDropdownAnswerComponent,
    ChoiceDirective,
    QuestionPreviewDirective,
    DynamicChoiceComponent,
    DynamicQuestionPreviewComponent,
    ModalWindowPreviewQuestionComponent,
    TrueFalsePreviewComponent,
    MultiSelectPreviewComponent,
    LongAnswerComponent,
    LongAnswerPreviewComponent,
    CommingSoonComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialModule,
    MatNativeDateModule,
    MatTooltipModule,
	
    // Vex
    VexModule,
    CustomLayoutModule,


    //TOTO: check copy-pasted from VEX ui single page - not sure if all of them are required
    PageLayoutModule,
    SecondaryToolbarModule,
    MatButtonModule,
    FlexLayoutModule,
    BreadcrumbsModule,
    IconModule,
    MatIconModule,
    ContainerModule,
    //TOTO: check

    QuillModule.forRoot({
      modules: {
        toolbar: [
          ['bold', 'italic', 'underline', 'strike'],        // toggled buttons
          ['blockquote', 'code-block'],

          [{ header: 1 }, { header: 2 }],               // custom button values
          [{ list: 'ordered' }, { list: 'bullet' }],
          [{ script: 'sub' }, { script: 'super' }],      // superscript/subscript
          [{ indent: '-1' }, { indent: '+1' }],          // outdent/indent
          [{ direction: 'rtl' }],                         // text direction

          [{ size: ['small', false, 'large', 'huge'] }],  // custom dropdown
          [{ header: [1, 2, 3, 4, 5, 6, false] }],

          [{ color: [] }, { background: [] }],          // dropdown with defaults from theme
          [{ align: [] }],

          ['clean'],                                         // remove formatting button

          ['link', 'image', 'video']                         // link and image, video
        ]
      }
    })
  ],
  entryComponents: [
    QuestionListComponent,
    QuestionPageComponent,
    ModalWindowPreviewQuestionComponent,
    MultipleChoiceAnswerComponent,
    TrueFalseAnswerComponent,
    MultiSelectChoiceComponent,
    LongAnswerComponent,
    TrueFalsePreviewComponent,
    MultiSelectPreviewComponent,
    LongAnswerPreviewComponent
  ],
  providers: [
    AuthGuard,
    QuizService,
    QuestionService,
    QuizResolver,
    QuestionResolver,
    AttemptService
  ],
  bootstrap: [
    AppComponent
  ]
} )
export class AppModule {
}
