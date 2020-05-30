import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomLayoutComponent } from './custom-layout/custom-layout.component';

import { CommingSoonComponent } from './common/comming-soon/comming-soon.component';

const routes: Routes = [
  {
    path: '',
    component: CustomLayoutComponent,
    children: []
  },
  {
    path: 'comming-soon',
    component: CommingSoonComponent,
    children: []
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    // preloadingStrategy: PreloadAllModules,
    scrollPositionRestoration: 'enabled',
    relativeLinkResolution: 'corrected',
    anchorScrolling: 'enabled'
  })],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
