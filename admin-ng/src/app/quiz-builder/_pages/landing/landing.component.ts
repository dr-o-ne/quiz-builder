import { Component, ViewEncapsulation } from '@angular/core';
import { FuseConfigService } from '@fuse/services/config.service';

@Component({
    selector: 'app-landing',
    templateUrl: './landing.component.html',
    styleUrls: ['./landing.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class LandingComponent {


    constructor(
        private fuseConfigService: FuseConfigService,
    ) {
        // Configure the layout
        this.fuseConfigService.config = {
            layout: {
                navbar: {
                    hidden: true
                },
                toolbar: {
                    hidden: true
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
