import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';
import { FuseSharedModule } from '@fuse/shared.module';
import { ContactComponent } from './contact/contact.component';
import { FeaturesComponent } from './features/features.component';
import { HelpComponent } from './help/help.component';
import { HomeComponent } from './home/home.component';
import { PlansComponent } from './plans/plans.component';

const routes = [
    { path: 'home', component: HomeComponent },
    { path: 'features', component: FeaturesComponent },
    { path: 'help', component: HelpComponent },
    { path: 'contact', component: ContactComponent },
    { path: 'plans', component: PlansComponent }
];

@NgModule({
    declarations: [
        HomeComponent,
        FeaturesComponent,
        HelpComponent,
        ContactComponent,
        PlansComponent
    ],
    imports: [
        RouterModule.forChild(routes),

        MatButtonModule,
        MatDividerModule,
        MatIconModule,

        FuseSharedModule
    ]
})
export class LandingModule {
}
