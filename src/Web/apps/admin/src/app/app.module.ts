import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import * as env from '../environments/environment';

import { AppComponent } from './app.component';

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
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(routes, { initialNavigation: 'enabled' }),
    SpinnerModule,
    StoreModule.forRoot({}),
    StoreDevtoolsModule.instrument({
      maxAge: 25, // Retains last 25 states
      logOnly: env.environment.production, // Restrict extension to log-only mode
    }),
    EffectsModule.forRoot([]),
    SharedUiModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
