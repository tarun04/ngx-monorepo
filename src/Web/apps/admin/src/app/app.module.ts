import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';

import { SpinnerModule } from '@ngx-monorepo/shared/framework/spinner';
import { SharedUiModule } from '@ngx-monorepo/shared/ui';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'analytics',
  },
  {
    path: 'analytics',
    loadChildren: () =>
      import('@ngx-monorepo/admin/analytics-page').then(
        (module) => module.AdminAnalyticsPageModule
      ),
  },
  {
    path: 'crm',
    loadChildren: () =>
      import('@ngx-monorepo/admin/crm-page').then(
        (module) => module.AdminCrmPageModule
      ),
  },
  {
    path: 'ecommerce',
    loadChildren: () =>
      import('@ngx-monorepo/admin/ecommerce-page').then(
        (module) => module.AdminEcommercePageModule
      ),
  },
  {
    path: 'projects',
    loadChildren: () =>
      import('@ngx-monorepo/admin/projects-page').then(
        (module) => module.AdminProjectsPageModule
      ),
  },
];

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(routes, { initialNavigation: 'enabled' }),
    SpinnerModule,
    SharedUiModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
