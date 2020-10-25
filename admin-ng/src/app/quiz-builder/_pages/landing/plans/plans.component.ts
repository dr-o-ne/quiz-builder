import { Component, ViewEncapsulation } from '@angular/core';
import { FuseConfigService } from '@fuse/services/config.service';
import { AuthService } from 'app/quiz-builder/services/auth/auth.service';

@Component({
    selector: 'app-plans',
    templateUrl: './plans.component.html',
    styleUrls: ['./plans.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class PlansComponent {

    constructor(
        private fuseConfigService: FuseConfigService,
        private authService: AuthService
    ) {
        // Configure the layout
        this.fuseConfigService.config = {
            layout: {
                navbar: {
                    hidden: !authService.isUserLoggedIn()
                },
                toolbar: {
                    hidden: false
                },
                footer: {
                    hidden: true
                },
                sidepanel: {
                    hidden: true
                }
            }
        };
    }

}
