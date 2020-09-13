import { NgModule } from '@angular/core';
import { DebounceClickDirective } from './utils/directives/debounceClick.directive';

@NgModule({
    declarations: [
        DebounceClickDirective
    ],
    imports: [
    ],
    exports: [
        DebounceClickDirective
    ]
})
export class CommonUtilsModule {
}
