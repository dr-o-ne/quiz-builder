import { Component, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { StartPageInfo } from 'app/quiz-builder/model/startPageInfo';

@Component({
    selector: 'qb-start-page-info',
    templateUrl: './start-page-info.component.html',
    styleUrls: ['./start-page-info.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class StartPageInfoComponent {

    startPageInfo!: StartPageInfo;

    constructor(
        private route: ActivatedRoute,
        private router: Router
    ) {
        this.startPageInfo = this.route.snapshot.data.startPageInfo;
        if (!this.startPageInfo.isStartPageEnabled) {
            this.router.navigate(['../'], { relativeTo: this.route });
        }
    }

}