import { Component, Input, ViewEncapsulation } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { fuseAnimations } from '@fuse/animations';
import { Quiz } from 'app/quiz-builder/model/quiz';

@Component({
    selector: 'app-quiz-info-appearance',
    templateUrl: './quiz-info-appearance.component.html',
    styleUrls: ['./quiz-info-appearance.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class QuizInfoEppearancePageComponent {

    @Input() form!: FormGroup;

    get headerColor(): string {
        return this.form.value.headerColor;
    }

    set headerColor(value: string) { 
        this.form.value.headerColor = value;
    }

    get backgroundColor(): string {
        return this.form.value.backgroundColor;
    }

    set backgroundColor(value: string) { 
        this.form.value.backgroundColor = value;
    }

    get sidesColor(): string {
        return this.form.value.sidesColor;
    }
    
    set sidesColor(value: string) { 
        this.form.value.sidesColor = value;
    }

    get footerColor(): string {
        return this.form.value.footerColor;
    }
    
    set footerColor(value: string) { 
        this.form.value.footerColor = value;
    }

    saveFormData(quiz: Quiz): void {
        const value = this.form.value;

        this.form.get('headerColor').value;

        console.log(value.headerColor as string);

    }

}