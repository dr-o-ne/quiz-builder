import { Component, ViewEncapsulation } from '@angular/core';
import { FuseConfigService } from '@fuse/services/config.service';
import { AuthService } from 'app/quiz-builder/services/auth/auth.service';

@Component({
    selector     : 'app-pricing',
    templateUrl  : './pricing.component.html',
    styleUrls    : ['./pricing.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class PricingComponent
{

    constructor(
        private fuseConfigService: FuseConfigService,
        private authService: AuthService
    ) {
        // Configure the layout
        this.fuseConfigService.config = {
            layout: {
                navbar: {
                    hidden: !this.authService.isUserLoggedIn()
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
